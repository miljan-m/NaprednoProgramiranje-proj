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

namespace LibraryApp.Tests.CityTests;


public class CityClassTest
{
    [Fact]
    public void Constructor_ValidInput_CreatesCity()
    {
        var city = new City("11000", "Belgrade");

        Assert.NotNull(city);
        Assert.Equal("11000", city.PostalCode);
        Assert.Equal("Belgrade", city.CityName);
    }

    [Fact]
    public void Constructor_PostalCodeIsNullOrEmpty_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new City("", "Belgrade"));
        Assert.Throws<ArgumentException>(() => new City(null, "Belgrade"));
    }

    [Fact]
    public void Constructor_CityNameIsNullOrEmpty_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new City("11000", ""));
        Assert.Throws<ArgumentException>(() => new City("11000", null));
    }
}