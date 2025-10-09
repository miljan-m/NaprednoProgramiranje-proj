
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

namespace LibraryApp.Tests;


public class AuthorServiceTest
{
    //testovi za konstruktor
    [Fact]
    public void Constructor_InputOk_CreatesValidAuthor()
    {
        var author = new Author("Miljan", "Mitic", new DateTime(1999, 5, 10));

        Assert.NotNull(author);
        Assert.Equal("Miljan", author.Name);
        Assert.Equal("Mitic", author.LastName);
        Assert.Equal(new DateTime(1999, 5, 10), author.DateOfBirth);
    }

    [Fact]
    public void Constructor_NameIsNullOrEmpty_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Author("", "Mitic"));
        Assert.Throws<ArgumentException>(() => new Author(null, "Mitic"));
    }

    [Fact]
    public void Constructor_LastNameIsNullOrEmpty_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Author("Miljan", ""));
        Assert.Throws<ArgumentException>(() => new Author("Miljan", null));
    }

    [Fact]
    public void Constructor_DateOfBirthInFuture_ThrowsException()
    {
        var futureDate = DateTime.Now.AddYears(1);
        Assert.Throws<ArgumentException>(() => new Author("Miljan", "Mitic", futureDate));
    }


    //testovi za validatore
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




    // testovi za servise

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
    

    [Fact]
        public async Task WriteJSONInFile_ShouldCreateJsonFile_WithCorrectContent()
        {
            var author = new Author("John", "Doe", new DateTime(1980, 1, 1))
            {
                AuthorId = "1"
            };
            var service = new JSONAuthorService<Author>();
            var fileName = "AuthorJsonFile.json";

            service.WriteJSONInFile(author);
            Assert.True(File.Exists(fileName), "JSON file was not created.");

            var fileContent = await File.ReadAllTextAsync(fileName);
            var deserializedAuthor = JsonSerializer.Deserialize<Author>(fileContent);

            Assert.NotNull(deserializedAuthor);
            Assert.Equal(author.AuthorId, deserializedAuthor.AuthorId);
            Assert.Equal(author.Name, deserializedAuthor.Name);
            Assert.Equal(author.LastName, deserializedAuthor.LastName);
            Assert.Equal(author.DateOfBirth, deserializedAuthor.DateOfBirth);

            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }


    
