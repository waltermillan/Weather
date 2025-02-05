// Infrastructure/Services/WeatherService.cs
using Core.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Infrastructure/Services/WeatherService.cs
        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            var apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?key=4HH5ZAQNNQMXA7TMLHC3Y4NFC&unitGroup=metric&iconSet=icons1&lang=es";
            var response = await _httpClient.GetStringAsync(apiUrl);

            // Deserializar la respuesta JSON a nuestro modelo
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

            // Acceder a los datos del primer día en la respuesta (puedes ajustar esto según lo que necesites)
            var weatherApiData = apiResponse?.Days?.FirstOrDefault();

            if (weatherApiData != null)
            {
                // Mapear los datos de la respuesta a nuestro modelo de dominio
                return new WeatherData
                {
                    Location = city,
                    Temperature = weatherApiData.Temp,
                    TempMax = weatherApiData.TempMax,
                    TempMin = weatherApiData.TempMin,
                    Humidity = weatherApiData.Humidity,
                    PrecipProb = weatherApiData.PrecipProb,
                    Sunrise = weatherApiData.Sunrise,
                    Sunset = weatherApiData.Sunset,
                    Description = weatherApiData.Description,  // Asignar la descripción del clima
                    Pressure = weatherApiData.Pressure,
                    Icon = weatherApiData.Icon
                };
            }

            return null;  // Si no se pueden encontrar datos
        }


    }
}
