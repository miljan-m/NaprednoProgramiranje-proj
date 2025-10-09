
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using System.Reflection;
using Moq;
using Xunit;
using LibraryApp.Application.Validators;
using FluentValidation.TestHelper;
using LibraryApp.Application.DTOs.ResponseDTO.Admin;
using LibraryApp.Mappers;
using LibraryApp.Application.DTOs.RequestDTO.Admin;
using LibraryApp.Application.Services.JSONServices;
using System.Text.Json;

namespace LibraryApp.Tests.AdminTests;


public class AdminJSONTest
{

    public async Task WriteJSONInFile_WritesCorrectAdminJson()
    {
        var service = new JSONAdminService<Admin>();
        var admin = new Admin("1", "Miljan", "Mitic", new DateTime(1990, 1, 1));
        var fileName = "AdminJsonFile.json";

         service.WriteJSONInFile(admin);
        Assert.True(File.Exists(fileName), "JSON file should exist after writing.");

        var fileContent = await File.ReadAllTextAsync(fileName);
        var deserialized = JsonSerializer.Deserialize<Admin>(fileContent);

        Assert.NotNull(deserialized);
        Assert.Equal(admin.AdminId, deserialized.AdminId);
        Assert.Equal(admin.FirstName, deserialized.FirstName);
        Assert.Equal(admin.LastName, deserialized.LastName);
        Assert.Equal(admin.DateOfBirth, deserialized.DateOfBirth);

        File.Delete(fileName);
    }
}