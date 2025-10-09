
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

public class AdminClassTest
{
    [Fact]
    public void Constructor_InputOk_CreatesValidAdmin()
    {
        var admin = new Admin("1", "Miljan", "Mitic", null);
        Assert.NotNull(admin);
        Assert.Equal("1", admin.AdminId);
        Assert.Equal("Miljan", admin.FirstName);
        Assert.Equal("Mitic", admin.LastName);
        Assert.Equal(null, admin.DateOfBirth);
    }

    [Fact]
    public void Constructor_NameIsNullOrEmpty_ThowsExcpetion()
    {
        Assert.Throws<ArgumentException>(() => new Admin("1", "", "Mitic", null));
        Assert.Throws<ArgumentException>(() => new Admin("1", null, "Mitic", null));
    }


    [Fact]
    public void Constructor_LastNameIsNullOrEmpty_ThowsExcpetion()
    {
        Assert.Throws<ArgumentException>(() => new Admin("1", "Miljan", "", null));
        Assert.Throws<ArgumentException>(() => new Admin("1", "Miljan", null, null));
    }


    [Fact]
    public void Constructor_IdIsNullOrEmpty_ThowsExcpetion()
    {
        Assert.Throws<ArgumentException>(() => new Admin("", "Miljan", "Mitic", null));
        Assert.Throws<ArgumentException>(() => new Admin(null, "Miljan", "Mitic", null));
    }
}