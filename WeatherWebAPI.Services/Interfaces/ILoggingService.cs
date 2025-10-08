using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebAPI.Services.Interfaces;

/// <summary>
/// Logging info collection service
/// </summary>
public interface ILoggingService
{
    Task LogRequestAsync(string clientIP, string cityRequested, string responseStatus, double responseTimeMs, string? errorMessage = null);
}
