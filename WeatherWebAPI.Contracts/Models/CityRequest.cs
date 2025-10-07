using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebAPI.Contracts.Models;

/// <summary>
/// Client to API request model with validation
/// </summary>
public class CityRequest
{
    [Required(ErrorMessage = "City name is required")] // useless annotation, cause IsNullOrWhiteSpace in client repeats main cycle with no data sending, but just in case
    [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ\s\-']+$", ErrorMessage = "City name contains invalid characters")] // city name must contains only letters or rarely dash
    public required string City { get; set; }
}
