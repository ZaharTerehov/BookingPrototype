using AspNetCore.ReCaptcha;
using Booking.ApplicationCore.Models;
using Booking.Infrastructure.Data;
using Booking.Web.Configuration;
using Booking.Web.Extentions;
using Booking.Web.Models.Account;
using Booking.Web.Services.Account;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net;

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

//Captcha
builder.Services.Configure<CaptchaConfig>(builder.Configuration.GetSection("ReCaptcha"));
builder.Services.AddHttpClient();

//JwtToken
var sectionJwtSettings = builder.Configuration.GetSection("JwtSettings");
var options = sectionJwtSettings.Get<JwtOptions>();

var jwtOptions = new JwtOptions(options.SigningKey, options.Issuer, options.Audience, 
    options.AccessTokenExpiryInMinutes, options.RefreshTokenExpiryInMinutes);

builder.Services.AddJwtAuthentication(builder.Configuration, jwtOptions);

builder.Services.Configure<JwtOptions>(sectionJwtSettings);

var app = builder.Build();

app.UseStatusCodePages(async context => {
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
        response.Redirect("/Account/Login");
});

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
    MinimumSameSitePolicy = SameSiteMode.None,
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