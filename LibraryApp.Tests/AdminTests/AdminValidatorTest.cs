
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;
using LibraryApp.Application.Validators;
using FluentValidation.TestHelper;
using LibraryApp.Application.DTOs.ResponseDTO.Admin;
using LibraryApp.Mappers;
using LibraryApp.Application.DTOs.RequestDTO.Admin;
using LibraryApp.Application.Services.JSONServices;
using System.Text.Json;

namespace LibraryApp.Tests.AdminTests;

public class AdminValidatorTest
{
    [Fact]
    public void Validator_AllOK_ValidatorNoError()
    {
        var admin = new Admin("1", "Miljan", "Mitic", null);
        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_IdEmptyOrNull_ValidatoError()
    {
        var admin = new Admin();
        admin.AdminId = "";
        admin.FirstName = "Miljan";
        admin.LastName = "Mitic";

        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldHaveValidationErrorFor(x => x.AdminId);
    }

    [Fact]
    public void Validator_NameEmptyOrNull_ValidatorError()
    {
        var admin = new Admin();
        admin.AdminId = "";
        admin.FirstName = "";
        admin.LastName = "Mitic";

        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validator_LastNameEmptyOrNull_ValidatorError()
    {
        var admin = new Admin();
        admin.AdminId = "";
        admin.FirstName = "Miljan";
        admin.LastName = "";

        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldHaveValidationErrorFor(x => x.LastName);

    }


}