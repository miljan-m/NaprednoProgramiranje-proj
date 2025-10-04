
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;

namespace LibraryApp.Tests;


public class AuthorServiceTest
{
    [Fact]
    public async Task GetAuthor_ById_ReturnsAuthor()
    {
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

        mockAuthorRepo.Setup(c => c.GetOneAsync("1")).ReturnsAsync(new Author { Name="Name",LastName="LastName",AuthorId="1"});

        var AuthorService = new AuthorService(mockAuthorRepo.Object);


        var result = await AuthorService.GetAuthor("1");
        Assert.NotNull(result);
        Assert.Equal("Name",result.Name);
        Assert.Equal("LastName", result.LastName);
    }
}