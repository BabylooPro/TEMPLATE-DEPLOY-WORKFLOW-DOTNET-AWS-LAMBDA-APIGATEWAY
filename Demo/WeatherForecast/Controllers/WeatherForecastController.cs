using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // STATIC ARROW OF WEATHER CUMMARIES FOR RANDOMIZATION
    private static readonly string[] Summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching",
    };

    private readonly ILogger<WeatherForecastController> _logger; // LOGGER INSTANCE FOR LOGGING REQUESTS

    // CONSTRUCTOR TO INJECT LOGGER DEPENDENCY
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    // GET ENDPOINT TO RETURN A WEATHER FORECAST LIST
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecastItem> Get()
    {
        // GENERATE A LIST OF RANDOM WEATHER FORECAST DATA
        return Enumerable
            .Range(1, 5)
            .Select(index => new WeatherForecastItem
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            })
            .ToArray();
    }
}
