using Microsoft.Extensions.Logging;
using WeatherWebAPI.Services.Data;
using WeatherWebAPI.Services.Interfaces;
using WeatherWebAPI.Contracts.Models;

namespace WeatherWebAPI.Services.Implementations;

public class LoggingService(ApplicationDbContext context, ILogger<LoggingService> logger) : ILoggingService
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<LoggingService> _logger = logger;

    /// <summary>
    /// Implementation of logging information collector
    /// </summary>
    /// <param name="clientIP"></param>
    /// <param name="cityRequested"></param>
    /// <param name="responseStatus"></param>
    /// <param name="responseTimeMs"></param>
    /// <param name="errorMessage"></param>
    /// <returns>Saves logging information to database</returns>
    public async Task LogRequestAsync(string clientIP, string cityRequested, string responseStatus, double responseTimeMs, string? errorMessage = null)
    {
        try
        {
            var logEntry = new LogEntry
            {
                ClientIP = clientIP,
                CityRequested = cityRequested,
                RequestTime = DateTime.UtcNow,
                ResponseStatus = responseStatus,
                ResponseTimeMs = responseTimeMs,
                ErrorMessage = errorMessage
            };

            _context.Logs.Add(logEntry);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to log request to database");
        }
    }
}
