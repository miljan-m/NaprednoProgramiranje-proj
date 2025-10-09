using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;
using LibraryApp.Application.DTOs.RequestDTO.Customer;
using LibraryApp.Mappers;
using LibraryApp.Application.Validators;
using FluentValidation.TestHelper;
using System.Text.Json;
using LibraryApp.Application.Services.JSONServices;

namespace LibraryApp.Tests.CustomerTests;


public class CustomerClassTest
{
     [Fact]
    public void Constructor_ValidInput_CreatesCustomer()
    {
        var customer = new Customer("Miljan", "Mitic", "1234567890123");

        Assert.NotNull(customer);
        Assert.Equal("Miljan", customer.FirstName);
        Assert.Equal("Mitic", customer.LastName);
        Assert.Equal("1234567890123", customer.jmbg);
    }

    [Fact]
    public void Constructor_InvalidFirstName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Customer("", "Mitic", "1234567890123"));
        Assert.Throws<ArgumentException>(() => new Customer(null, "Mitic", "1234567890123"));
    }

    [Fact]
    public void Constructor_InvalidLastName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Customer("Miljan", "", "1234567890123"));
        Assert.Throws<ArgumentException>(() => new Customer("Miljan", null, "1234567890123"));
    }

    [Fact]
    public void Constructor_InvalidJMBG_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Customer("Miljan", "Mitic", ""));
        Assert.Throws<ArgumentException>(() => new Customer("Miljan", "Mitic", null));
        Assert.Throws<ArgumentException>(() => new Customer("Miljan", "Mitic", "123")); // manje od 13 cifara
        Assert.Throws<ArgumentException>(() => new Customer("Miljan", "Mitic", "123456789012A")); // ne-numerički
    }
}

