using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Contracts.Models;
using WeatherWebAPI.Services.Interfaces;

namespace WeatherWebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherResponse>> GetWeather(string city)
    {
        try
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
