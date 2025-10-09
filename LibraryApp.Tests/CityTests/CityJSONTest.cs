
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


public class CityJSONTest
{
    [Fact]
        public async Task WriteJSONInFile_ShouldCreateJsonFile_WithCorrectContent()
        {
            var city = new City("11000", "Belgrade");
            var service = new JSONCityService<City>();
            var fileName = "CityJsonFile.json";

             service.WriteJSONInFile(city);

            Assert.True(File.Exists(fileName), "JSON file was not created.");

            var fileContent = await File.ReadAllTextAsync(fileName);
            var deserializedCity = JsonSerializer.Deserialize<City>(fileContent);

            Assert.NotNull(deserializedCity);
            Assert.Equal(city.PostalCode, deserializedCity.PostalCode);
            Assert.Equal(city.CityName, deserializedCity.CityName);

            if (File.Exists(fileName))
                File.Delete(fileName);
        }

}
