
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

namespace LibraryApp.Tests;


public class AdminServiceTest
{

    //testovi za konstruktor
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


    //testovi za validatore
    [Fact]
    public void Validator_AllOK_ValidatorNoError()
    {
        var admin = new Admin("1", "Miljan", "Mitic", null);
        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_IdEmptyOrNull_ValidatoError()
    {
        var admin = new Admin();
        admin.AdminId = "";
        admin.FirstName = "Miljan";
        admin.LastName = "Mitic";

        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldHaveValidationErrorFor(x => x.AdminId);
    }

    [Fact]
    public void Validator_NameEmptyOrNull_ValidatorError()
    {
        var admin = new Admin();
        admin.AdminId = "";
        admin.FirstName = "";
        admin.LastName = "Mitic";

        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validator_LastNameEmptyOrNull_ValidatorError()
    {
        var admin = new Admin();
        admin.AdminId = "";
        admin.FirstName = "Miljan";
        admin.LastName = "";

        var validator = new AdminValidator();
        var result = validator.TestValidate(admin);
        result.ShouldHaveValidationErrorFor(x => x.LastName);

    }


    //testovi za servise
    [Fact]
    public async Task GetAdmin_ById_ReturnsAdmin()
    {
        var mockAdminRepo = new Mock<IGenericRepository<Admin>>();

        mockAdminRepo.Setup(c => c.GetOneAsync("1")).ReturnsAsync(new Admin { FirstName = "Name", LastName = "LastName", AdminId = "1" });

        var AdminService = new AdminService(mockAdminRepo.Object);


        var result = await AdminService.GetAdmin("1");
        Assert.NotNull(result);
        Assert.Equal("Name", result.FirstName);
        Assert.Equal("LastName", result.LastName);
    }


    [Fact]
    public async void GettAllAdmins_ReturnsAllAdmins()
    {
        var admins = new List<Admin>()
        {
            new("1","Miljan","Mitic",null),
            new("2","Mirko","Mirkovic",null)
        };
        var mockAdminRepo = new Mock<IGenericRepository<Admin>>();
        mockAdminRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(admins);
        var adminService = new AdminService(mockAdminRepo.Object);

        var result = await adminService.GetAdmins();
        Assert.Equal(2, ((List<GetAdminsDTO>)result).Count);
    }


    [Fact]
    public async Task DeleteAdmin_ShouldDeleteAdmin_WhenAdminExists()
    {
        var admin = new Admin("1", "Miljan", "Mitic", null);
        var mockAdminRepo = new Mock<IGenericRepository<Admin>>();
        mockAdminRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(admin);
        var adminService = new AdminService(mockAdminRepo.Object);
        mockAdminRepo.Setup(r => r.DeleteAsync("1")).ReturnsAsync(true);

        var result = await adminService.DeleteAdmin("1");
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAdmin_ShouldUpdate_WhenAdminExists()
    {
        var mockAdminRepo = new Mock<IGenericRepository<Admin>>();
        var adminToUpdate = new Admin
        {
            FirstName = "Mirko",
            LastName = "Mirkovic"
        };
        var a = new Admin("1", "Mirko", "Mirkovic", null);

        var admin = new Admin("1", "Miljan", "Mitic", null);

        mockAdminRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(admin);
        mockAdminRepo.Setup(r => r.UpdateAsync(It.Is<Admin>(a => a.FirstName == "Mirko" && a.LastName == "Mirkovic"), "1")).ReturnsAsync(adminToUpdate);

        var adminService = new AdminService(mockAdminRepo.Object);
        var adminToUpdateDTO = new UpdateAdminDTO
        {
            FirstName = "Mirko",
            LastName = "Mirkovic",
            DateOfBirth = null
        };
        var result = await adminService.UpdateAdmin("1", adminToUpdateDTO);
        Assert.NotNull(result);
        Assert.Equal("Mirko", result.FirstName);
        Assert.Equal("Mirkovic", result.LastName);


    }

    [Fact]
    public async Task CreateAdmin_ShouldCreateAdmin_WhenValidInput()
    {
        var mockAdminRepo = new Mock<IGenericRepository<Admin>>();
        var adminService = new AdminService(mockAdminRepo.Object);

        var createAdminDTO = new CreateAdminDTO
        {
            FirstName = "Miljan",
            LastName = "Mitic",
            DateOfBirth = null
        };

        var domainAdmin = new Admin
        {
            FirstName = "Miljan",
            LastName = "Mitic",
            DateOfBirth = null
        };

        var createdAdmin = new Admin("1", "Miljan", "Mitic", null);

        mockAdminRepo.Setup(r => r.CreateAsync(It.IsAny<Admin>())).ReturnsAsync(createdAdmin);

        var result = await adminService.CreateAdmin(createAdminDTO);

        Assert.NotNull(result);
        Assert.Equal("Miljan", result.FirstName);
        Assert.Equal("Mitic", result.LastName);
    }
    
    
    [Theory]
    [InlineData("1", "1", true)]
    [InlineData("1", "2", false)]
    [InlineData(null, "1", false)]
    public void Admin_Equals_ReturnsExpected(string id1, string id2, bool expected)
    {
        var admin1 = new Admin { AdminId = id1 };
        var admin2 = new Admin { AdminId = id2 };

        Assert.Equal(expected, admin1.Equals(admin2));
    }
}