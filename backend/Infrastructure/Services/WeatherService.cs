using Core.Interfaces;
using Core.Models;
using Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _weatherUrl;
    private readonly string _urlVariables;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AesEncryptionService _encryptionService;

    public WeatherService(HttpClient httpClient, IConfiguration configuration, IUnitOfWork unitOfWork, AesEncryptionService encryptionService)
    {
        _httpClient = httpClient;
        _weatherUrl = configuration["WeatherInfo:URL"];
        _urlVariables = configuration["WeatherInfo:UrlVariables"];
        _unitOfWork = unitOfWork;
        _encryptionService = encryptionService;
    }

    public async Task<WeatherData?> GetWeatherDataAsync(string city)
    {
        var apiKey = await _unitOfWork.ApiKeys.GetKeyByProviderAsync("VisualCrossing");

        if (apiKey == null)
            throw new Exception("API Key not found.");

        apiKey.Key = _encryptionService.Decrypt(apiKey.Key);

        var apiUrl = $"{_weatherUrl}/{city}?key={apiKey.Key}{_urlVariables}";
        var response = await _httpClient.GetStringAsync(apiUrl);

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
        var weatherApiData = apiResponse?.Days?.FirstOrDefault();

        if (weatherApiData != null)
        {
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
                Description = weatherApiData.Description,
                Pressure = weatherApiData.Pressure,
                Icon = weatherApiData.Icon
            };
        }

        return null;
    }

    public async Task<List<WeatherData>> GetForecastWeatherDataAsync(string city, string date1, string date2)
    {
        var apiKey = await _unitOfWork.ApiKeys.GetKeyByProviderAsync("VisualCrossing");

        if (apiKey is null)
            throw new Exception("API Key not found.");

        apiKey.Key = _encryptionService.Decrypt(apiKey.Key);

        var apiUrl = $"{_weatherUrl}/{city}/{date1}/{date2}?key={apiKey.Key}{_urlVariables}";
        var response = await _httpClient.GetStringAsync(apiUrl);

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

        if (apiResponse?.Days != null && apiResponse.Days.Any())
        {
            var weatherList = apiResponse.Days.Select(day => new WeatherData
            {
                Datetime = day.DateTime,
                Location = city,
                Temperature = day.Temp,
                TempMax = day.TempMax,
                TempMin = day.TempMin,
                Humidity = day.Humidity,
                PrecipProb = day.PrecipProb,
                Sunrise = day.Sunrise,
                Sunset = day.Sunset,
                Description = day.Description,
                Pressure = day.Pressure,
                Icon = day.Icon
            }).ToList();

            return weatherList;
        }

        return new List<WeatherData>();
    }
}
