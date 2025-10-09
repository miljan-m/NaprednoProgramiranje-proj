
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


public class BookJSONTest
{

    [Fact]
        public async Task WriteJSONInFile_ShouldCreateJsonFile_WithCorrectContent()
        {
            var book = new Book("12345", "Sample Book", "Fiction", true, "1")
            {
                Author = new Author("John", "Doe", new DateTime(1980, 1, 1)) { AuthorId = "1" }
            };

            var service = new JSONBookService<Book>();

            var fileName = "BookJsonFile.json";

             service.WriteJSONInFile(book);

            Assert.True(File.Exists(fileName), "JSON file was not created.");

            var fileContent = await File.ReadAllTextAsync(fileName);
            var deserializedBook = JsonSerializer.Deserialize<Book>(fileContent);

            Assert.NotNull(deserializedBook);
            Assert.Equal(book.Isbn, deserializedBook.Isbn);
            Assert.Equal(book.Title, deserializedBook.Title);
            Assert.Equal(book.Genre, deserializedBook.Genre);
            Assert.Equal(book.Available, deserializedBook.Available);
            Assert.Equal(book.AuthorId, deserializedBook.AuthorId);
            Assert.NotNull(deserializedBook.Author);
            Assert.Equal(book.Author.Name, deserializedBook.Author.Name);

            if (File.Exists(fileName))
            File.Delete(fileName);
        }
}
