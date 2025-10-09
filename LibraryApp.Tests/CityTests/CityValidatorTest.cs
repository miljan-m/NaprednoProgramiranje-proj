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


public class CityVAlidatorTest
{
    [Fact]
    public void Validator_ValidCity_NoValidationErrors()
    {
        var validator = new CityValidator();
        var city = new City("11000", "Belgrade");
        var result = validator.TestValidate(city);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_CityNameEmpty_ValidationError()
    {
        var city = new City();
        city.CityName = "";
        city.PostalCode = "17530";
        var validator = new CityValidator();

        var result = validator.TestValidate(city);

        result.ShouldHaveValidationErrorFor(x => x.CityName)
            .WithErrorMessage("City name cannot be empty string");
    }

    [Fact]
    public void Validator_PostalCodeNull_ValidationError()
    {
        var city = new City();
        city.CityName = "Beograd";
        city.PostalCode = null;
        var validator = new CityValidator();

        var result = validator.TestValidate(city);

        result.ShouldHaveValidationErrorFor(x => x.PostalCode).WithErrorMessage("Postal code cannot be null");
    }

    [Fact]
    public void Validator_PostalCodeEmpty_ValidationError()
    {
        var city = new City();
        city.CityName = "Beograd";
        city.PostalCode = "";
        var validator = new CityValidator();

        var result = validator.TestValidate(city);

        result.ShouldHaveValidationErrorFor(x => x.PostalCode).WithErrorMessage("Postal code is required");
    }

    [Fact]
    public void Validator_PostalCodeNonNumeric_ValidationError()
    {
        var city = new City("11A00", "Belgrade");
        var validator = new CityValidator();

        var result = validator.TestValidate(city);

        result.ShouldHaveValidationErrorFor(x => x.PostalCode).WithErrorMessage("Postal code must contain only numbers");
    }

}