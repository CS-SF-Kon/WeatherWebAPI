using WeatherWebAPI.Contracts.Models;

namespace WeatherWebAPI.Services.Interfaces;

/// <summary>
/// Weather info retreaving service
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// Weather info retreaving method
    /// </summary>
    /// <param name="city"></param>
    /// <returns>WeatherResponse model</returns>
    Task<WeatherResponse> GetWeatherAsync(string city);
}
