using System.Text.Json;
using WeatherWebAPI.Contracts.Models;

namespace Client;

internal class Program
{
    private static readonly HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Weather App ===");

        while (true)
        {
            Console.Write("\nEnter city name (or 'quit' to exit): ");
            var city = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(city))
                continue;

            if (city.ToLower() == "quit")
                break;

            try
            {
                var response = await httpClient.GetAsync($"https://localhost:7120/weather/{city}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var weather = JsonSerializer.Deserialize<WeatherResponse>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    Console.WriteLine($"\nWeather in {weather.City}:");
                    Console.WriteLine($"Temperature: {weather.Temperature}°C");
                    Console.WriteLine($"Description: {weather.Description}");
                    Console.WriteLine($"Humidity: {weather.Humidity}%");
                    Console.WriteLine($"Wind Speed: {weather.WindSpeed} km/h");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Make sure the WebAPI is running!");
            }
        }
    }
}
