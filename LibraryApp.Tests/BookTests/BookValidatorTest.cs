
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

public class BookValidatorClass
{
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

}
