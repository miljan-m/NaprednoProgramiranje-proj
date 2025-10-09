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


public class CustomerValidatorTest
{
    

    [Fact]
    public void Validator_ValidCustomer_NoValidationErrors()
    {
        var customer = new Customer("Miljan", "Mitic", "1234567890123");
        var validator = new CustomerValidator();
        var result = validator.TestValidate(customer);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_FirstNameEmpty_ValidationError()
    {
        var customer = new Customer();
        customer.FirstName = "";
        customer.LastName = "Mitic";
        customer.jmbg = "1706002744116";
        var validator = new CustomerValidator();

        var result = validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("First name must be entered");
    }

    [Fact]
    public void Validator_LastNameEmpty_ValidationError()
    {
        var customer = new Customer();
        customer.FirstName = "Miljan";
        customer.LastName = "";
        customer.jmbg = "1706002744116";
        var validator = new CustomerValidator();

        var result = validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage("Last name cannot be empty string");
    }

    [Fact]
    public void Validator_JMBGInvalidLength_ValidationError()
    {
        var customer = new Customer();
        customer.FirstName = "Miljan";
        customer.LastName = "";
        customer.jmbg = "1asd";
        var validator = new CustomerValidator();

        var result = validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(x => x.jmbg).WithErrorMessage("JMBG must be 13 diggit number");
    }


}