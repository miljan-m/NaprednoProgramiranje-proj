
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

namespace LibraryApp.Tests.BookTests;


public class BookClassTest
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
