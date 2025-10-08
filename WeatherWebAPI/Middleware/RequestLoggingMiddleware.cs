using WeatherWebAPI.Services.Interfaces;

namespace WeatherWebAPI.Middleware;

/// <summary>
/// provides logger
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ILoggingService loggingService)
    {
        var startTime = DateTime.UtcNow;
        var clientIP = GetClientIP(context);
        var cityRequested = GetCityFromRequest(context);

        try
        {
            await _next(context);

            var responseTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
            var statusCode = context.Response.StatusCode.ToString();

            if (context.Response.StatusCode < 500)
            {
                await loggingService.LogRequestAsync(clientIP, cityRequested, statusCode, responseTime);
            }
        }
        catch (Exception ex)
        {
            var responseTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
            await loggingService.LogRequestAsync(clientIP, cityRequested, "500", responseTime, ex.Message);
            throw;
        }
    }

    private static string GetClientIP(HttpContext context)
    {
        return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }

    private static string GetCityFromRequest(HttpContext context)
    {
        if (context.Request.RouteValues["city"] is string city)
        {
            return city;
        }
        return "Unknown";
    }
}
