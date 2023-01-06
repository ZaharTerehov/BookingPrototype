using Booking.Infrastructure.Data;
using Booking.Web.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ApartmentType}/{action=Index}/{id?}");

app.Run();
