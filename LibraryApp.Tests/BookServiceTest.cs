
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

namespace LibraryApp.Tests;


public class BookServiceTest
{
    public class BookTests
    {
        // testovi za konstruktor

        [Fact]
        public void Constructor_InputOk_CreatesValidBook()
        {
            var book = new Book("12345", "Na Drini Cuprija", "Roman", true, "1");

            Assert.NotNull(book);
            Assert.Equal("12345", book.Isbn);
            Assert.Equal("Na Drini Cuprija", book.Title);
            Assert.Equal("Roman", book.Genre);
            Assert.True(book.Available);
            Assert.Equal("1", book.AuthorId);
        }

        [Fact]
        public void Constructor_IsbnIsNullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Book("", "Na Drini Cuprija", "Roman", true));
            Assert.Throws<ArgumentException>(() => new Book(null, "Na Drini Cuprija", "Roman", true));
        }

        [Fact]
        public void Constructor_TitleIsNullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Book("12345", "", "Roman", true));
            Assert.Throws<ArgumentException>(() => new Book("12345", null, "Roman", true));
        }

        [Fact]
        public void Method_BookDetails_ReturnsExpectedString()
        {
            var book = new Book("12345", "Prokleta Avlija", "Roman", false);

            var result = book.BookDetails();

            Assert.Contains("ISBN: 12345", result);
            Assert.Contains("Title: Prokleta Avlija", result);
            Assert.Contains("Genre: Roman", result);
            Assert.Contains("Available: False", result);
        }

        [Fact]
        public void Method_ShowAutograph_ReturnsExpectedMessage()
        {
            var book = new Book("12345", "Prokleta Avlija", "Roman", true);
            var result = book.ShowAutograph();
            Assert.Equal("This is not special edition book. Autograph IS NOT available", result);
        }
    }


    // testovi za validatore

        [Fact]
        public void Validator_AllFieldsValid_NoValidationErrors()
        {
            var validator = new BookValidator();
            var book = new Book
            {
                AuthorId = "1",
                Isbn = "12345",
                Title = "Na Drini Cuprija",
                Genre = "Roman"
            };

            var result = validator.TestValidate(book);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_AuthorIdEmpty_ValidationError()
        {
            var validator = new BookValidator();

            var book = new Book
            {
                AuthorId = "",
                Isbn = "12345",
                Title = "Book",
                Genre = "Roman"
            };

            var result = validator.TestValidate(book);
            result.ShouldHaveValidationErrorFor(x => x.AuthorId).WithErrorMessage("Author id must be entered");
        }

        [Fact]
        public void Validator_IsbnEmpty_ValidationError()
        {
            var validator = new BookValidator();

            var book = new Book
            {
                AuthorId = "1",
                Isbn = "",
                Title = "Book",
                Genre = "Roman"
            };

            var result = validator.TestValidate(book);
            result.ShouldHaveValidationErrorFor(x => x.Isbn).WithErrorMessage("ISBN cannot be empty string");
        }

        [Fact]
        public void Validator_TitleEmpty_ValidationError()
        {
            var validator = new BookValidator();

            var book = new Book
            {
                AuthorId = "1",
                Isbn = "12345",
                Title = "",
                Genre = "Roman"
            };

            var result = validator.TestValidate(book);
            result.ShouldHaveValidationErrorFor(x => x.Title).WithErrorMessage("Title cannot be empty string");
        }

        [Fact]
        public void Validator_GenreEmpty_ValidationError()
        {
            var validator = new BookValidator();

            var book = new Book
            {
                AuthorId = "1",
                Isbn = "12345",
                Title = "Book",
                Genre = ""
            };

            var result = validator.TestValidate(book);
            result.ShouldHaveValidationErrorFor(x => x.Genre).WithErrorMessage("Genre cannot be empty string");
        }


    // testovi za servise

        [Fact]
        public async Task GetBook_ById_ReturnsBook()
        {
            var mockBookRepo = new Mock<IGenericRepository<Book>>();
            var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

            mockBookRepo.Setup(c => c.GetOneAsync("12345"))
                .ReturnsAsync(new Book { Isbn = "12345", Title = "Book", Genre = "Drama", AuthorId = "1" });

            var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object);

            var result = await bookService.GetBook("12345");

            Assert.NotNull(result);
            Assert.Equal("Book", result.Title);
            Assert.Equal("Drama", result.Genre);
        }

        [Fact]
        public async Task GetBooks_ReturnsAllBooks()
        {
            var books = new List<Book>
            {
                new("111", "Book1", "Genre1", true),
                new("222", "Book2", "Genre2", false)
            };

            var mockBookRepo = new Mock<IGenericRepository<Book>>();
            var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

            mockBookRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(books);

            var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object);

            var result = await bookService.GetBooks();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteBook_ShouldDeleteBook_WhenBookExists()
        {
            var book = new Book("111", "Book1", "Genre1", true);
            var mockBookRepo = new Mock<IGenericRepository<Book>>();
            var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

            mockBookRepo.Setup(r => r.GetOneAsync("111")).ReturnsAsync(book);
            mockBookRepo.Setup(r => r.DeleteAsync("111")).ReturnsAsync(true);

            var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object);

            var result = await bookService.DeleteBook("111");

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateBook_ShouldUpdate_WhenBookExists()
        {
            var mockBookRepo = new Mock<IGenericRepository<Book>>();
            var mockAuthorRepo = new Mock<IGenericRepository<Author>>();

            var existingBook = new Book("123", "OldTitle", "OldGenre", true);
            var updatedBookEntity = new Book("123", "NewTitle", "NewGenre", true);

            mockBookRepo.Setup(r => r.GetOneAsync("123")).ReturnsAsync(existingBook);
            mockBookRepo.Setup(r => r.UpdateAsync(It.IsAny<Book>(), "123")).ReturnsAsync(updatedBookEntity);

            var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object);

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

            var author = new Author("Ivo", "Andric");
            mockAuthorRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(author);

            var bookService = new BookService(mockBookRepo.Object, mockAuthorRepo.Object);

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
