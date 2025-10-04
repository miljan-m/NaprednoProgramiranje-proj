
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;

namespace LibraryApp.Tests;


public class BookServiceTest
{
    [Fact]
    public async Task GetBook_ByPostalCode_ReturnsBook()
    {
        var mockBookRepo = new Mock<IGenericRepository<Book>>();
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

        mockBookRepo.Setup(c => c.GetOneAsync("17530")).ReturnsAsync(new Book { Available = true, Genre = "Adventure", Title = "Alice in Wonderland", Isbn = "a98f7531as" });

        var BookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object);


        var result = await BookService.GetBook("17530");
        Assert.NotNull(result);
        Assert.Equal("a98f7531as", result.Isbn);
        Assert.Equal("Alice in Wonderland", result.Title);
        Assert.Equal("Adventure", result.Genre);
        Assert.True(result.Available);
        
    }
}