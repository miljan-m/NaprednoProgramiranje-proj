
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


public class AuthorServiceTest
{
    

    [Fact]
    public async Task GetAuthor_ById_ReturnsAuthor()
    {
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var mockJSONService = new Mock<IJSONService<Author>>();
        mockAuthorRepo.Setup(c => c.GetOneAsync("1")).ReturnsAsync(new Author { AuthorId = "1", Name = "Ivo", LastName = "Andric" });

        var authorService = new AuthorService(mockAuthorRepo.Object,mockJSONService.Object);

        var result = await authorService.GetAuthor("1");

        Assert.NotNull(result);
        Assert.Equal("Ivo", result.Name);
        Assert.Equal("Andric", result.LastName);
    }

    [Fact]
    public async Task GetAllAuthors_ReturnsAllAuthors()
    {   
        var mockJSONService = new Mock<IJSONService<Author>>();
        
        var authors = new List<Author>
            {
                new("Ivo", "Andric", new DateTime(1892, 10, 9)),
                new("Mesa", "Selimovic", new DateTime(1910, 4, 26))
            };

        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        mockAuthorRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(authors);

        var authorService = new AuthorService(mockAuthorRepo.Object,mockJSONService.Object);

        var result = await authorService.GetAuthors();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteAuthor_ShouldDeleteAuthor_WhenAuthorExists()
    {
        var author = new Author("Ivo", "Andric", null);
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var mockJSONService = new Mock<IJSONService<Author>>();
        
        mockAuthorRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(author);
        mockAuthorRepo.Setup(r => r.DeleteAsync("1")).ReturnsAsync(true);

        var authorService = new AuthorService(mockAuthorRepo.Object,mockJSONService.Object);

        var result = await authorService.DeleteAuthor("1");

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAuthor_ShouldUpdate_WhenAuthorExists()
    {
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var mockJSONService = new Mock<IJSONService<Author>>();

        var existingAuthor = new Author("Ivo", "Andric", null);
        var updatedAuthor = new Author("Mesa", "Selimovic", null);

        mockAuthorRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(existingAuthor);
        mockAuthorRepo.Setup(r => r.UpdateAsync(updatedAuthor, "1")).ReturnsAsync(updatedAuthor);

        var authorService = new AuthorService(mockAuthorRepo.Object,mockJSONService.Object);

        var updateDto = new AuthorUpdateDTO
        {
            Name = "Mesa",
            LastName = "Selimovic",
            DateOfBirth = null
        };

        var result = await authorService.UpdateAuthor("1", updateDto);

        Assert.NotNull(result);
        Assert.Equal("Mesa", result.Name);
        Assert.Equal("Selimovic", result.LastName);
    }

    [Fact]
    public async Task CreateAuthor_ShouldCreateAuthor_WhenValidInput()
    {
        var mockJSONService = new Mock<IJSONService<Author>>();

        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var authorService = new AuthorService(mockAuthorRepo.Object,mockJSONService.Object);

        var createDto = new AuthorCreateDTO
        {
            Name = "Ivo",
            LastName = "Andric",
            DateOfBirth = new DateTime(1892, 10, 9)
        };

        var createdAuthor = new Author("Ivo", "Andric", new DateTime(1892, 10, 9))
        {
            AuthorId = "1"
        };

        mockAuthorRepo.Setup(r => r.CreateAsync(createdAuthor)).ReturnsAsync(createdAuthor);

        var result = await authorService.CreateAuthor(createDto);

        Assert.NotNull(result);
        Assert.Equal("Ivo", result.Name);
        Assert.Equal("Andric", result.LastName);
    }

    [Theory]
    [InlineData("1", "1", true)]
    [InlineData("1", "2", false)]
    public void Author_Equals_ReturnsExpected(string id1, string id2, bool expected)
    {
        var author1 = new Author { AuthorId = id1 };
        var author2 = new Author { AuthorId = id2 };

        Assert.Equal(expected, author1.Equals(author2));
    }
    

    }


    
