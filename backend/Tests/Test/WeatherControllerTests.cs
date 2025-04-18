using API.Controllers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Core.Models;

namespace Tests.Test
{
    public class WeatherControllerTests
    {
        [Fact]
        public async Task GetWeather_ShouldReturnWeatherData()
        {
            var mockService = new Mock<WeatherService>();
            mockService.Setup(service => service.GetWeatherDataAsync(It.IsAny<string>()))
                       .ReturnsAsync(new WeatherData
                       {
                           Location = "",
                           Temperature = 10.7f,
                           TempMax = 15.9f,
                           TempMin = 5.9f,
                           Humidity = 78,
                           PrecipProb = 100,
                           Sunrise = "08:17:03",
                           Sunset = "18:46:44",
                           Description = "Similar temperatures continuing with no rain expected."
                       });

            var controller = new WeathersController(mockService.Object);

            var result = await controller.GetWeather("Malaga");

            var okResult = result as OkObjectResult;
            var weatherData = okResult.Value as WeatherData;

            Assert.NotNull(weatherData);
            Assert.Equal("Malaga", weatherData.Location);
            Assert.Equal(22.5f, weatherData.Temperature);
        }
    }
}
