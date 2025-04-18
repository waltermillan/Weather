using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WeathersController : BaseApiController
{
    private readonly WeatherService _weatherService;

    public WeathersController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    // Endpoint for getting weather data for 1 day, and 1 city 
    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        try
        {
            var weather = await _weatherService.GetWeatherDataAsync(city);

            if (weather is null)
                return NotFound($"No data found for the city {city}");

            return Ok(weather);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }

    // Endpoint for getting forecast data for 3 days, and 1 city 
    [HttpGet("forecast/{city}")]
    public async Task<IActionResult> GetForecatsWeather(string city, string date1, string date2)
    {
        try
        {
            var weather = await _weatherService.GetForecastWeatherDataAsync(city, date1, date2);

            if (weather is null)
                return NotFound($"No data found for the city {city}");

            return Ok(weather);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
        }
    }
}
