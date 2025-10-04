
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;

namespace LibraryApp.Tests;


public class AddressServiceTest
{
    [Fact]
    public async Task GetAddress_ByPostalCode_ReturnsAddress()
    {
        var mockAddressRepo = new Mock<IGenericRepository<Address>>();
        var mockCityRepo = new Mock<IGenericRepository<City>>();

        mockAddressRepo.Setup(c => c.GetOneAsync("17530")).ReturnsAsync(new Address { id = "1", number = 1, street = "Milutina Stojanovica", PostalCode = "17530" });

        var AddressService = new AddressService(mockAddressRepo.Object, mockCityRepo.Object);


        var result = await AddressService.GetAddress("17530");
        Assert.NotNull(result);
        Assert.Equal("1", result.id);
        Assert.Equal(1, result.number);
        Assert.Equal("17530", result.PostalCode);
        
    }
}