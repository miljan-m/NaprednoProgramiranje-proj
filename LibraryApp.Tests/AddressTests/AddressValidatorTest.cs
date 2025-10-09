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

public class AddressValidatorTest
{
    [Fact]
    public void Validator_AllOk_ValidatorNoError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = 5,
            street = "Milutina Stojanovica"
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_NumberInputInvalid_ValidatorError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = -10,
            street = "Milutina Stojanovica"
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.number);
    }

    [Fact]
    public void Validator_StreetInputEmptyInvalid_ValidatorError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = 1,
            street = ""
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.street);
    }

    [Fact]
    public void Validator_StreetInputNullInvalid_ValidatorError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = 1,
            street = null
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.street);
    }
}