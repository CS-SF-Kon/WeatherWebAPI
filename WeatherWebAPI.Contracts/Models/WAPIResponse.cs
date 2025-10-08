using System.Text.Json.Serialization;

namespace WeatherWebAPI.Contracts.Models;

/// <summary>
/// WeatherAPI response structure
/// </summary>
public class WAPIResponse
{
    [JsonPropertyName("location")]
    public Location Location { get; set; } = new();

    [JsonPropertyName("current")]
    public Current Current { get; set; } = new();
}

public class Location
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class Current
{
    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; } // quite risky, but experience has shown that humidity is integer 

    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; } = new();
}

public class Condition
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}