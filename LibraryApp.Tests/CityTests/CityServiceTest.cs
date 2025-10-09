
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;
using LibraryApp.Application.Validators;
using FluentValidation.TestHelper;
using LibraryApp.Application.Services.JSONServices;
using System.Text.Json;

namespace LibraryApp.Tests;


public class CityServiceTest
{

    

    [Fact]
    public async Task GetCity_ReturnsCity_WhenCityExists()
    {
        var mockJSONService = new Mock<IJSONService<City>>();

        var mockRepo = new Mock<IGenericRepository<City>>();
        var city = new City("11000", "Belgrade");

        mockRepo.Setup(r => r.GetOneAsync("11000")).ReturnsAsync(city);

        var service = new CityService(mockRepo.Object, mockJSONService.Object);

        var result = await service.GetCity("11000");

        Assert.NotNull(result);
        Assert.Equal("Belgrade", result.CityName);
    }

    [Fact]
    public async Task GetCities_ReturnsAllCities()
    {
        var mockJSONService = new Mock<IJSONService<City>>();

        var cities = new List<City>
            {
                new("11000", "Belgrade"),
                new("21000", "Novi Sad")
            };

        var mockRepo = new Mock<IGenericRepository<City>>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(cities);

        var service = new CityService(mockRepo.Object, mockJSONService.Object);

        var result = await service.GetCities();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateCity_ShouldReturnCity()
    {
        var mockJSONService = new Mock<IJSONService<City>>();

        var city = new City("11000", "Belgrade");

        var mockRepo = new Mock<IGenericRepository<City>>();
        mockRepo.Setup(r => r.CreateAsync(city)).ReturnsAsync(city);

        var service = new CityService(mockRepo.Object, mockJSONService.Object);

        var result = await service.CreateCity(city);

        Assert.NotNull(result);
        Assert.Equal("Belgrade", result.CityName);
    }

    [Fact]
    public async Task DeleteCity_ShouldReturnTrue()
    {
        var mockJSONService = new Mock<IJSONService<City>>();

        var mockRepo = new Mock<IGenericRepository<City>>();
        mockRepo.Setup(r => r.DeleteAsync("11000")).ReturnsAsync(true);

        var service = new CityService(mockRepo.Object, mockJSONService.Object);

        var result = await service.DeleteCity("11000");

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateCity_ShouldReturnUpdatedCity_WhenCityExists()
    {
        var existingCity = new City("11000", "Belgrade");
        var updatedCity = new City("11000", "New Belgrade");
        var mockJSONService = new Mock<IJSONService<City>>();

        var mockRepo = new Mock<IGenericRepository<City>>();
        mockRepo.Setup(r => r.GetOneAsync("11000")).ReturnsAsync(existingCity);
        mockRepo.Setup(r => r.UpdateAsync(updatedCity, "11000")).ReturnsAsync(updatedCity);

        var service = new CityService(mockRepo.Object, mockJSONService.Object);

        var result = await service.UpdateCity("11000", updatedCity);

        Assert.NotNull(result);
        Assert.Equal("New Belgrade", result.CityName);
        Assert.Equal("11000", result.PostalCode);
    }

    [Theory]
    [InlineData("11000", "11000", true)]
    [InlineData("11000", "21000", false)]
    public void City_Equals_ReturnsExpected(string code1, string code2, bool expected)
    {
        var city1 = new City { PostalCode = code1 };
        var city2 = new City { PostalCode = code2 };

        Assert.Equal(expected, city1.Equals(city2));
    }

}
