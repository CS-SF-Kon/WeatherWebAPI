using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherWebAPI.Contracts.Models;
using WeatherWebAPI.Services.Interfaces;

namespace WeatherWebAPI.Services.Implementations;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<WAPISettings> _settings;


    public WeatherService(HttpClient httpClient, IOptions<WAPISettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    /// <summary>
    /// Implementarion of weather information method.
    /// Accepts name of city.
    /// Now has real WeatherAPI retriever.
    /// </summary>
    /// <param name="city"></param>
    /// <returns>WeatherResponse model</returns>
    public async Task<WeatherResponse> GetWeatherAsync(string city)
    {
        var url = $"{_settings.Value.BaseUrl}v1/current.json?key={_settings.Value.ApiKey}&q={city}&aqi=no";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();

            throw new Exception($"Weather service error: {response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WAPIResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString |
                           JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        return new WeatherResponse { 
            City= weatherData.Location.Name,
            Temperature =  weatherData.Current.TempC,
            Description =  weatherData.Current.Condition.Text,
            Humidity = weatherData.Current.Humidity,
            WindSpeed =  weatherData.Current.WindKph,
        };
    }
}
