
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using WeatherWebAPI.Contracts.Models;
using WeatherWebAPI.Middleware;
using WeatherWebAPI.Services.Data;
using WeatherWebAPI.Services.Implementations;
using WeatherWebAPI.Services.Interfaces;

namespace WeatherWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Host.UseNLog(); // reconfiguring logger to NLog

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=weather.db")); // simple target for logs reording - SQLite database

        builder.Services.Configure<WAPISettings>(
            builder.Configuration.GetSection("WAPI"));

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        builder.Services.AddScoped<ILoggingService, LoggingService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
