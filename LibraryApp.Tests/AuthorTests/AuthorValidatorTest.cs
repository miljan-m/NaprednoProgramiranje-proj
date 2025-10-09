using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using Moq;
using Xunit;
using LibraryApp.Application.DTOs.ResponseDTO.Authors;
using LibraryApp.Application.DTOs.RequestDTO.Author;
using LibraryApp.Application.Validators;
using FluentValidation.TestHelper;
using System.Text.Json;
using LibraryApp.Application.Services.JSONServices;

namespace LibraryApp.Tests.AuthorTests;

public class AuthorValidatorTest
{
    [Fact]
    public void Validator_AllFieldsValid_NoValidationErrors()
    {
        var author = new Author
        {
            Name = "Ivo",
            LastName = "Andric",
            DateOfBirth = new DateTime(1892, 10, 9)
        };
        var validator = new AuthorValidator();
        var result = validator.TestValidate(author);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_NameIsNull_ValidationError()
    {
        var validator = new AuthorValidator();

        var author = new Author
        {
            Name = null,
            LastName = "Andric",
            DateOfBirth = new DateTime(1892, 10, 9)
        };

        var result = validator.TestValidate(author);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name cannot be null");
    }

    [Fact]
    public void Validator_NameIsEmpty_ValidationError()
    {
        var validator = new AuthorValidator();

        var author = new Author
        {
            Name = "",
            LastName = "Andric",
            DateOfBirth = new DateTime(1892, 10, 9)
        };

        var result = validator.TestValidate(author);

        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Name cannot be empty string");
    }

    [Fact]
    public void Validator_LastNameIsNull_ValidationError()
    {
        var validator = new AuthorValidator();

        var author = new Author
        {
            Name = "Ivo",
            LastName = null,
            DateOfBirth = new DateTime(1892, 10, 9)
        };

        var result = validator.TestValidate(author);

        result.ShouldHaveValidationErrorFor(x => x.LastName).WithErrorMessage("LastName cannot be null");
    }

    [Fact]
    public void Validator_LastNameIsEmpty_ValidationError()
    {
        var validator = new AuthorValidator();

        var author = new Author
        {
            Name = "Ivo",
            LastName = "",
            DateOfBirth = new DateTime(1892, 10, 9)
        };

        var result = validator.TestValidate(author);

        result.ShouldHaveValidationErrorFor(x => x.LastName).WithErrorMessage("Last name cannot be empty string");
    }


}