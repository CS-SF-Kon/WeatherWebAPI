namespace WeatherWebAPI.Services.Interfaces;

/// <summary>
/// Logging info collection service
/// </summary>
public interface ILoggingService
{
    Task LogRequestAsync(string clientIP, string cityRequested, string responseStatus, double responseTimeMs, string? errorMessage = null);
}
