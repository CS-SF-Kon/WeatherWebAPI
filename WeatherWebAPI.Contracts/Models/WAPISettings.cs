namespace WeatherWebAPI.Contracts.Models;

/// <summary>
/// WeatherAPI settings model
/// </summary>
public class WAPISettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
