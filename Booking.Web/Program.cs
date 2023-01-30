using Booking.Infrastructure.Data;
using Booking.Web.Configuration;
using Booking.Web.Extentions;
using Booking.Web.Services.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Booking.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

#region Serilog Configure
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCoreServices();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//JwtToken
var sectionJwtSettings = builder.Configuration.GetSection("JwtSettings");
var options = sectionJwtSettings.Get<JwtOptions>();

var jwtOptions = new JwtOptions(options.SigningKey, options.Issuer, options.Audience, options.TokenExpiryInMinutes);

builder.Services.AddJwtAuthentication(jwtOptions);

builder.Services.Configure<JwtOptions>(sectionJwtSettings);

var app = builder.Build();

app.Logger.LogInformation("Database migraion running...");
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var bookingContext = scopedProvider.GetRequiredService<BookingContext>();
        if (bookingContext.Database.IsSqlServer())
        {
            bookingContext.Database.Migrate();
        }
        await BookingContextSeed.SeedAsync(bookingContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to Databse.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Improves cookie security
app.UseCookiePolicy(new CookiePolicyOptions
{
	MinimumSameSitePolicy = SameSiteMode.Strict,
	HttpOnly = HttpOnlyPolicy.Always,
	Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSecureJwt();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Apartment}/{action=Index}/{id?}");

app.Run();