using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebAPI.Contracts.Models;

public class WAPIResponse
{
    public Location Location { get; set; } = new();
    public Current Current { get; set; } = new();
}

public class Location
{
    public string Name { get; set; } = string.Empty;
}

public class Current
{
    public decimal TempC { get; set; }
    public double Humidity { get; set; }
    public float WindKph { get; set; }
    public Condition Condition { get; set; } = new();
}

public class Condition
{
    public string Text { get; set; } = string.Empty;
}