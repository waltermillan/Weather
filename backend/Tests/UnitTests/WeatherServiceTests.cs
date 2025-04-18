﻿using Infrastructure.Services;
using Moq;
using Core.Models;

namespace Tests.UnitTests;

public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherDataAsync_ShouldReturnValidData()
    {
        var mockHttpClient = new Mock<HttpClient>();
        var weatherService = new WeatherService(mockHttpClient.Object, null, null, null);

        var fakeWeatherData = new WeatherData
        {
            Location = "Malaga",
            Temperature = 22.5f,
            Description = "Sunny"
        };

        mockHttpClient.Setup(client => client.GetStringAsync(It.IsAny<string>()))
                       .ReturnsAsync(fakeWeatherData.ToString());

        var result = await weatherService.GetWeatherDataAsync("Malaga");

        Assert.NotNull(result);
        Assert.Equal("Malaga", result.Location);
        Assert.Equal(22.5f, result.Temperature);
        Assert.Equal("Sunny", result.Description);
    }
}
