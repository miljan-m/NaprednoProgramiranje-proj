
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;

namespace LibraryApp.Tests;


public class CustomerServiceTest
{
    [Fact]
    public async Task GetCustomer_ByJMBG_ReturnsCustomer()
    {
        var mockCustomerRepo = new Mock<IGenericRepository<Customer>>();

        mockCustomerRepo.Setup(c => c.GetOneAsync("17530")).ReturnsAsync(new Customer { FirstName="Name",JMBG="1706002744116",LastName="LastName"});

        var CustomerService = new CustomerService(mockCustomerRepo.Object);


        var result = await CustomerService.GetCustomer("17530");
        Assert.NotNull(result);
        Assert.Equal("Name",result.FirstName);
        Assert.Equal("LastName", result.LastName);
    }
}