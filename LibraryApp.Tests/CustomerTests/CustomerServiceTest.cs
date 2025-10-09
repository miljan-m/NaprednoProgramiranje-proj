
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


public class CustomerServiceTest
{
   


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

}