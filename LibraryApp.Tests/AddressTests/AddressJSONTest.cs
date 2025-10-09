
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


public class AddressJSONTest
{
    [Fact]
    public async Task WriteJSONInFile_WritesCorrectJson()
    {
        var service = new JSONAddressService<Address>();
        var testAddress = new Address
        {
            street = "Main Street",
            number = 123
        };
        var fileName = "AddressJsonFile.json";

        service.WriteJSONInFile(testAddress);

        Assert.True(File.Exists(fileName), "JSON file should exist after writing.");

        var fileContent = await File.ReadAllTextAsync(fileName);
        var deserialized = JsonSerializer.Deserialize<Address>(fileContent);

        Assert.NotNull(deserialized);
        Assert.Equal(testAddress.street, deserialized.street);
        Assert.Equal(testAddress.number, deserialized.number);

        File.Delete(fileName);
    }
}