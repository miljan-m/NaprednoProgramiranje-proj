
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

namespace LibraryApp.Tests;


public class CustomerServiceTest
{
    //testovi za konstruktor
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

// testovi za validatore

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


    // testovi za servise


    [Fact]
    public async Task GetCustomer_ReturnsCustomer_WhenExists()
    {
        var mockRepo = new Mock<IGenericRepository<Customer>>();
        var customer = new Customer("Miljan", "Mitic", "1234567890123");
        mockRepo.Setup(r => r.GetOneAsync("1234567890123")).ReturnsAsync(customer);
        var mockJSONService = new Mock<IJSONService<Customer>>();

        var service = new CustomerService(mockRepo.Object, mockJSONService.Object);

        var result = await service.GetCustomer("1234567890123");

        Assert.NotNull(result);
        Assert.Equal("Miljan", result.FirstName);
    }

    [Fact]
    public async Task GetCustomers_ReturnsAllCustomers()
    {
        var customers = new List<Customer>
            {
                new Customer("Miljan","Mitic","1234567890123"),
                new Customer("Mirko","Mirkovic","9876543210987")
            };

        var mockRepo = new Mock<IGenericRepository<Customer>>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);
        var mockJSONService = new Mock<IJSONService<Customer>>();

        var service = new CustomerService(mockRepo.Object, mockJSONService.Object);

        var result = await service.GetCustomers();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnCustomer()
    {
        var mockJSONService = new Mock<IJSONService<Customer>>();

        var customerDto = new CreateCustomerDTO
        {
            FirstName = "Miljan",
            LastName = "Mitic",
            JMBG = "1234567890123"
        };
        var customerEntity = customerDto.MapDtoToDomainEntity();

        var mockRepo = new Mock<IGenericRepository<Customer>>();
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(customerEntity);

        var service = new CustomerService(mockRepo.Object, mockJSONService.Object);

        var result = await service.CreateCustomer(customerDto);

        Assert.NotNull(result);
        Assert.Equal("Miljan", result.FirstName);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnTrue_WhenCustomerExists()
    {
        var customer = new Customer("Miljan", "Mitic", "1234567890123");
        var mockJSONService = new Mock<IJSONService<Customer>>();

        var mockRepo = new Mock<IGenericRepository<Customer>>();
        mockRepo.Setup(r => r.GetOneAsync("1234567890123")).ReturnsAsync(customer);
        mockRepo.Setup(r => r.DeleteAsync("1234567890123")).ReturnsAsync(true);

        var service = new CustomerService(mockRepo.Object, mockJSONService.Object);

        var result = await service.DeleteCustomer("1234567890123");

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldReturnUpdatedCustomer_WhenExists()
    {
        var mockJSONService = new Mock<IJSONService<Customer>>();

        var existingCustomer = new Customer("Miljan", "Mitic", "1234567890123");
        var updateDto = new UpdateCustomerDTO
        {
            FirstName = "Mirko",
            LastName = "Mirkovic"
        };

        var mockRepo = new Mock<IGenericRepository<Customer>>();
        mockRepo.Setup(r => r.GetOneAsync("1234567890123")).ReturnsAsync(existingCustomer);
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Customer>(), "1234567890123")).ReturnsAsync(existingCustomer);

        var service = new CustomerService(mockRepo.Object, mockJSONService.Object);

        var result = await service.UpdateCustomer(updateDto, "1234567890123");

        Assert.NotNull(result);
        Assert.Equal("Mirko", result.FirstName);
        Assert.Equal("Mirkovic", result.LastName);
    }



    [Theory]
    [InlineData("1234567890123", "1234567890123", true)]
    [InlineData("1234567890123", "9876543210987", false)]
    public void Customer_Equals_ReturnsExpected(string jmbg1, string jmbg2, bool expected)
    {
        var c1 = new Customer { jmbg = jmbg1 };
        var c2 = new Customer { jmbg = jmbg2 };

        Assert.Equal(expected, c1.Equals(c2));
    }

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