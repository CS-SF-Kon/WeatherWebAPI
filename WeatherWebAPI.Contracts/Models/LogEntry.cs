namespace WeatherWebAPI.Contracts.Models;

/// <summary>
/// Logging entry that will be recodred
/// </summary>
public class LogEntry
{
    public int Id { get; set; }

    public string ClientIP { get; set; } = string.Empty;

    public string CityRequested { get; set; } = string.Empty;

    public DateTime RequestTime { get; set; }

    public string? ResponseStatus { get; set; }

    public double? ResponseTimeMs { get; set; }

    public string? ErrorMessage { get; set; }
}
