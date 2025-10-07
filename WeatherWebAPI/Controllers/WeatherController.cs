using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherWebAPI.Contracts.Models;
using WeatherWebAPI.Services.Interfaces;

namespace WeatherWebAPI.Controllers;

/// <summary>
/// Main controller
/// </summary>
[Route("[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    /// <summary>
    /// Main answer method.
    /// Supports city name validation.
    /// </summary>
    /// <param name="city"></param>
    /// <returns>WeatherResponse model</returns>
    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherResponse>> GetWeather(string city)
    {
        var request = new CityRequest // City name validation via model attributes
        {
            City = city
        };

        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            return BadRequest(validationResults.Select(v => v.ErrorMessage)); // city name validation unsuccessfull
        }

        try
        {
            var weather = await _weatherService.GetWeatherAsync(city); // city name validation successfull
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message }); // another errors
        }
    }
}
