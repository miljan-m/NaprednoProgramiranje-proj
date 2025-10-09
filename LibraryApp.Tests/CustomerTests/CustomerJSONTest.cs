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


public class CustomerJSONTest
{
    [Fact]
    public async Task WriteJSONInFile_ShouldCreateJsonFile_WithCorrectContent()
    {
        var customer = new Customer("Miljan", "Mitic", "1234567890123");
        var service = new JSONCustomerService<Customer>();
        var fileName = "CustomerJsonFile.json";

        service.WriteJSONInFile(customer);

        Assert.True(File.Exists(fileName), "JSON file was not created.");

        var fileContent = await File.ReadAllTextAsync(fileName);
        var deserializedCustomer = JsonSerializer.Deserialize<Customer>(fileContent);

        Assert.NotNull(deserializedCustomer);
        Assert.Equal(customer.FirstName, deserializedCustomer.FirstName);
        Assert.Equal(customer.LastName, deserializedCustomer.LastName);
        Assert.Equal(customer.jmbg, deserializedCustomer.jmbg);

        if (File.Exists(fileName))
            File.Delete(fileName);
    }
}