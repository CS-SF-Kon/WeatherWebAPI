
using WeatherWebAPI.Services.Implementations;
using WeatherWebAPI.Services.Interfaces;
using WeatherWebAPI.Contracts.Models;

namespace WeatherWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<WAPISettings>(
            builder.Configuration.GetSection("WAPI"));

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IWeatherService, WeatherService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
