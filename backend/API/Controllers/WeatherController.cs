// API/Controllers/WeatherController.cs
using Core.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        // Inyección de dependencias del servicio de clima
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // Endpoint para obtener los datos del clima de una ciudad

        [HttpGet("Get")]
        public async Task<IActionResult> GetWeather(string city)
        {
            // Obtener los datos de clima de la ciudad
            var weatherData = await _weatherService.GetWeatherDataAsync(city);

            if (weatherData == null)
            {
                return NotFound($"No se encontraron datos para la ciudad {city}");
            }

            return Ok(weatherData);  // Devolvemos los datos del clima al cliente
        }

        [HttpGet("Get2")]
        public Task<IActionResult> GetWeather2(string city)
        {
            // Crea una instancia del modelo WeatherResponse con los datos
            var weather = new WeatherData
            {
                Location = "Murcia",
                Temperature = 10,
                TempMax = 17.4f,
                TempMin = 5,
                Humidity = 60.3f,
                PrecipProb = 0,
                Sunrise = "08:05:14",
                Sunset = "18:32:26",
                Description = "Condiciones claras.",
                Pressure = 1031.5f,
                Icon = "clear-day"
            };

            // Devuelve la respuesta en formato JSON utilizando Ok()
            return Task.FromResult<IActionResult>(Ok(weather));
        }

    }
}
