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

public class AuthorClassTest
{
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

}
