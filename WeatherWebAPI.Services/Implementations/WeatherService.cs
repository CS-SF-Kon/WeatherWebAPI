using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherWebAPI.Contracts.Models;
using WeatherWebAPI.Services.Interfaces;

namespace WeatherWebAPI.Services.Implementations;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Implementarion of weather information method.
    /// Accepts name of city.
    /// Has temporary stopper.
    /// </summary>
    /// <param name="city"></param>
    /// <returns>WeatherResponse model</returns>
    public async Task<WeatherResponse> GetWeatherAsync(string city)
    {
        return new WeatherResponse { // temporary stopper
            City = city,
            Temperature = 20.5,
            Description = "Sunny",
            Humidity = 65.0,
            WindSpeed = 15.2
        };
    }
}
