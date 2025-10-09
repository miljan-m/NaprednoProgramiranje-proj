
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


public class AuthorJSONTest
{
    
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