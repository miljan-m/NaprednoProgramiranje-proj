using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using Moq;
using Xunit;
using LibraryApp.Application.Validators;
using LibraryApp.Application.DTOs.RequestDTO.Address;
using FluentValidation.TestHelper;
using LibraryApp.Mappers;
using LibraryApp.Application.Services.JSONServices;
using System.Text.Json;
using LibraryApp.Application.Interfaces;

namespace LibraryApp.Tests.AddressTests;

public class AddressClassTests
{
    [Fact]
    public void Constructor_InputOk_CreatesValidAddress()
    {
        var city = new City("17530", "Surdulica");
        var address = new Address("1", 14, "Milutina Stojanovica", city, "17530");
        Assert.Equal("1", address.id);
        Assert.Equal(14, address.number);
        Assert.Equal("Milutina Stojanovica", address.street);
        Assert.Equal(city, address.City);
        Assert.Equal("17530", address.PostalCode);

    }

    [Fact]
    public void Constructor_IdNullOrEmpty_ThrowsException()
    {
        var city = new City("17530", "Surdulica");

        Assert.Throws<ArgumentException>(() => new Address("", 14, "Milutina Stojanovica", city, "17530"));
        Assert.Throws<ArgumentException>(() => new Address(null, 14, "Milutina Stojanovica", city, "17530"));

    }

    [Fact]
    public void Constructor_NumberLessThan1_ThrowsException()
    {
        var city = new City("17530", "Surdulica");

        Assert.Throws<ArgumentException>(() => new Address("1", 0, "Milutina Stojanovica", city, "17530"));
    }

    [Fact]
    public void Constructor_NullOrEmptyStreet_ThrowsException()
    {
        var city = new City("17530", "Surdulica");

        Assert.Throws<ArgumentException>(() => new Address("1", 14, null, city, "17530"));
        Assert.Throws<ArgumentException>(() => new Address("1", 14, "", city, "17530"));
    }

    [Fact]
    public void Constructor_NullCity_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Address("1", 14, "Milutina Stojanovica", null, "17530"));
    }

    [Fact]
    public void Constructor_NullOrEmptyPostalCode_ThrowsException()
    {
        var city = new City("17530", "Belgrade");

        Assert.Throws<ArgumentException>(() => new Address("1", 10, "Milutina Stojanovica", city, null));
        Assert.Throws<ArgumentException>(() => new Address("1", 10, "Milutina Stojanovica", city, ""));
    }

}