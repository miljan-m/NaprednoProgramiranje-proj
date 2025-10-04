
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;

namespace LibraryApp.Tests;


public class AdminServiceTest
{
    [Fact]
    public async Task GetAdmin_ById_ReturnsAdmin()
    {
        var mockAdminRepo = new Mock<IGenericRepository<Admin>>();

        mockAdminRepo.Setup(c => c.GetOneAsync("1")).ReturnsAsync(new Admin { FirstName="Name",LastName="LastName",AdminId="1"});

        var AdminService = new AdminService(mockAdminRepo.Object);


        var result = await AdminService.GetAdmin("1");
        Assert.NotNull(result);
        Assert.Equal("Name",result.FirstName);
        Assert.Equal("LastName", result.LastName);
    }
}