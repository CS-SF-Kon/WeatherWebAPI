using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebAPI.Contracts.Models;

/// <summary>
/// Our API response model
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
    public double Temperature { get; set; }

    /// <summary>
    /// Weather description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Air humidity
    /// </summary>
    public int Humidity { get; set; }

    /// <summary>
    /// Speed of wind
    /// </summary>
    public double WindSpeed { get; set; }
}
