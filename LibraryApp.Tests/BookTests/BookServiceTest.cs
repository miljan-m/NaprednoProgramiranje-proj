
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.DTOs.RequestDTO.Book;
using LibraryApp.Application.Validators;
using FluentValidation.TestHelper;
using System.Text.Json;
using LibraryApp.Application.Services.JSONServices;

namespace LibraryApp.Tests;


public class BookServiceTest
{

    [Fact]
    public async Task GetBook_ById_ReturnsBook()
    {
        var mockBookRepo = new Mock<IGenericRepository<Book>>();
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var mockJSONService = new Mock<IJSONService<Book>>();
        mockBookRepo.Setup(c => c.GetOneAsync("12345"))
            .ReturnsAsync(new Book { Isbn = "12345", Title = "Book", Genre = "Drama", AuthorId = "1" });

        var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object, mockJSONService.Object);

        var result = await bookService.GetBook("12345");

        Assert.NotNull(result);
        Assert.Equal("Book", result.Title);
        Assert.Equal("Drama", result.Genre);
    }

    [Fact]
    public async Task GetBooks_ReturnsAllBooks()
    {
        var mockJSONService = new Mock<IJSONService<Book>>();

        var books = new List<Book>
            {
                new("111", "Book1", "Genre1", true),
                new("222", "Book2", "Genre2", false)
            };

        var mockBookRepo = new Mock<IGenericRepository<Book>>();
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

        mockBookRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(books);

        var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object,mockJSONService.Object);

        var result = await bookService.GetBooks();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteBook_ShouldDeleteBook_WhenBookExists()
    {   
        var mockJSONService = new Mock<IJSONService<Book>>();

        var book = new Book("111", "Book1", "Genre1", true);
        var mockBookRepo = new Mock<IGenericRepository<Book>>();
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

        mockBookRepo.Setup(r => r.GetOneAsync("111")).ReturnsAsync(book);
        mockBookRepo.Setup(r => r.DeleteAsync("111")).ReturnsAsync(true);

        var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object,mockJSONService.Object);

        var result = await bookService.DeleteBook("111");

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateBook_ShouldUpdate_WhenBookExists()
    {
        var mockBookRepo = new Mock<IGenericRepository<Book>>();
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var mockJSONService = new Mock<IJSONService<Book>>();

        var existingBook = new Book("123", "OldTitle", "OldGenre", true);
        var updatedBookEntity = new Book("123", "NewTitle", "NewGenre", true);

        mockBookRepo.Setup(r => r.GetOneAsync("123")).ReturnsAsync(existingBook);
        mockBookRepo.Setup(r => r.UpdateAsync(It.IsAny<Book>(), "123")).ReturnsAsync(updatedBookEntity);

        var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object,mockJSONService.Object);

        var updateDto = new BookUpdateDTO
        {
            Title = "NewTitle",
            Genre = "NewGenre",
            Available = true
        };

        var result = await bookService.UpdateBook("123", updateDto);

        Assert.NotNull(result);
        Assert.Equal("NewTitle", result.Title);
        Assert.Equal("NewGenre", result.Genre);
    }

    [Fact]
    public async Task CreateBook_ShouldCreate_WhenAuthorExists()
    {
        var mockBookRepo = new Mock<IGenericRepository<Book>>();
        var mockAuthorRepo = new Mock<IGenericRepository<Author>>();
        var mockJSONService = new Mock<IJSONService<Book>>();

        var author = new Author("Ivo", "Andric");
        mockAuthorRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(author);

        var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object,mockJSONService.Object);

        var createDto = new BookCreateDTO
        {
            Title = "Na Drini Cuprija",
            Genre = "Roman",
            Available = true
        };

        var createdBook = new Book("12345", "Na Drini Cuprija", "Roman", true, "1");

        mockBookRepo.Setup(r => r.CreateAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);

        var result = await bookService.CreateBook(createDto, "1");

        Assert.NotNull(result);
        Assert.Equal("Na Drini Cuprija", result.Title);
        Assert.Equal("Roman", result.Genre);
    }

    [Theory]
    [InlineData("123", "123", true)]
    [InlineData("123", "456", false)]
    public void Book_Equals_ReturnsExpected(string isbn1, string isbn2, bool expected)
    {
        var book1 = new Book { Isbn = isbn1 };
        var book2 = new Book { Isbn = isbn2 };

        Assert.Equal(expected, book1.Equals(book2));
    }
}
