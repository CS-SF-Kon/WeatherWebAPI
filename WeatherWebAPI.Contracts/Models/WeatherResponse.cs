using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebAPI.Contracts.Models;

/// <summary>
/// WeatherAPI response model
/// </summary>
public class WeatherResponse
{
    /// <summary>
    /// City name
    /// </summary>
    public string City {  get; set; }

    /// <summary>
    /// Current temperature
    /// </summary>
    public decimal Temperature { get; set; }

    /// <summary>
    /// Weather description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Air humidity
    /// </summary>
    public double Humidity { get; set; }

    /// <summary>
    /// Speed of wind
    /// </summary>
    public float WindSpeed { get; set; }
}
