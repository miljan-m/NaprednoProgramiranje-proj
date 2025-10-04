
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;

namespace LibraryApp.Tests;


public class CityServiceTest
{
    [Fact]
    public async Task GetCity_ByPostalCode_ReturnsCity()
    {
        var mockCityRepo = new Mock<IGenericRepository<City>>();

        mockCityRepo.Setup(c => c.GetOneAsync("17530")).ReturnsAsync(new City { CityName="Surdulica",PostalCode="17530"});

        var cityService = new CityService(mockCityRepo.Object);


        var result = await cityService.GetCity("17530");
        Assert.NotNull(result);
        Assert.Equal("17530", result.PostalCode);
        Assert.Equal("Surdulica", result.CityName);
    }
}