using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WeatherWebAPI.Contracts.Models;

namespace Client;

internal class Program
{
    private static readonly HttpClient httpClient = new();

    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder() // loading configuration from appsettings.json
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var apiSettings = configuration.GetSection("ApiSettings");
        var baseUrl = apiSettings["BaseUrl"]; // it makes the API address reconfiguration flexible

        Console.WriteLine("=== Weather App ===");
        Console.WriteLine($"Using API: {baseUrl}"); // to make sure the API address is correct
        Console.WriteLine();

        while (true)
        {
            Console.Write("\nEnter city name (or 'quit' to exit): ");
            var city = Console.ReadLine(); // read cityname

            if (string.IsNullOrWhiteSpace(city)) // continue
                continue;

            if (city.Equals("quit", StringComparison.CurrentCultureIgnoreCase)) // quit
                break;

            try
            {
                var response = await httpClient.GetAsync($"{baseUrl}/weather/{city}");

                if (response.IsSuccessStatusCode) // if city name is valid
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var content = await response.Content.ReadAsStringAsync();
                    var weather = JsonSerializer.Deserialize<WeatherResponse>(content, options);

                    Console.WriteLine($"\nWeather in {weather.City}:");
                    Console.WriteLine($"Temperature: {weather.Temperature}°C");
                    Console.WriteLine($"Description: {weather.Description}");
                    Console.WriteLine($"Humidity: {weather.Humidity}%");
                    Console.WriteLine($"Wind Speed: {weather.WindSpeed} km/h");
                }
                else // if city name is not valid
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Make sure the WebAPI is running or address in appsettings.json is correct!");
            }
        }
    }
}
