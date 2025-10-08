
using LibraryApp.Domen.Models;
using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Application.Services;
using Moq;
using Xunit;
using LibraryApp.Application.Validators;
using LibraryApp.Application.DTOs.RequestDTO.Address;
using FluentValidation.TestHelper;
using LibraryApp.Mappers;

namespace LibraryApp.Tests;


public class AddressServiceTest
{
    //testovi za konstruktor

    [Fact]
    public void Constructor_InputOk_CreatesValidAddress()
    {
        var city = new City("17530", "Surdulica");
        var address = new Address("1", 14, "Milutina Stojanovica", city, "17530");
        Assert.Equal("1", address.id);
        Assert.Equal(14, address.number);
        Assert.Equal("Milutina Stojanovica", address.street);
        Assert.Equal(city, address.City);
        Assert.Equal("17530", address.PostalCode);

    }

    [Fact]
    public void Constructor_IdNullOrEmpty_ThrowsException()
    {
        var city = new City("17530", "Surdulica");

        Assert.Throws<ArgumentException>(() => new Address("", 14, "Milutina Stojanovica", city, "17530"));
        Assert.Throws<ArgumentException>(() => new Address(null, 14, "Milutina Stojanovica", city, "17530"));

    }

    [Fact]
    public void Constructor_NumberLessThan1_ThrowsException()
    {
        var city = new City("17530", "Surdulica");

        Assert.Throws<ArgumentException>(() => new Address("1", 0, "Milutina Stojanovica", city, "17530"));
    }

    [Fact]
    public void Constructor_NullOrEmptyStreet_ThrowsException()
    {
        var city = new City("17530", "Surdulica");

        Assert.Throws<ArgumentException>(() => new Address("1", 14, null, city, "17530"));
        Assert.Throws<ArgumentException>(() => new Address("1", 14, "", city, "17530"));
    }

    [Fact]
    public void Constructor_NullCity_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Address("1", 14, "Milutina Stojanovica", null, "17530"));
    }

    [Fact]
    public void Constructor_NullOrEmptyPostalCode_ThrowsException()
    {
        var city = new City("17530", "Belgrade");

        Assert.Throws<ArgumentException>(() => new Address("1", 10, "Milutina Stojanovica", city, null));
        Assert.Throws<ArgumentException>(() => new Address("1", 10, "Milutina Stojanovica", city, ""));
    }




    //testovi za validator
    [Fact]
    public void Validator_AllOk_ValidatorNoError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = 5,
            street = "Milutina Stojanovica"
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_NumberInputInvalid_ValidatorError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = -10,
            street = "Milutina Stojanovica"
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.number);
    }

    [Fact]
    public void Validator_StreetInputEmptyInvalid_ValidatorError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = 1,
            street = ""
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.street);
    }

    [Fact]
    public void Validator_StreetInputNullInvalid_ValidatorError()
    {
        var addresValidator = new AddressValidator();
        var dto = new AddressCreateDTO
        {
            id = "1",
            number = 1,
            street = null
        };
        var result = addresValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.street);
    }

    //testovi za servise

    [Fact]
    public async Task GetAddress_ByPostalCode_ReturnsAddress()
    {
        var mockAddressRepo = new Mock<IGenericRepository<Address>>();
        var mockCityRepo = new Mock<IGenericRepository<City>>();

        mockAddressRepo.Setup(c => c.GetOneAsync("17530")).ReturnsAsync(new Address { id = "1", number = 1, street = "Milutina Stojanovica", PostalCode = "17530" });

        var AddressService = new AddressService(mockAddressRepo.Object, mockCityRepo.Object);


        var result = await AddressService.GetAddress("17530");
        Assert.NotNull(result);
        Assert.Equal(1, result.number);
        Assert.Equal("17530", result.PostalCode);

    }

    [Fact]
    public async void GetAllAddresses_ReturnAllAddresses()
    {
        var mockAddressRepo = new Mock<IGenericRepository<Address>>();
        var mockCityRepo = new Mock<IGenericRepository<City>>();

        var city1 = new City("17530", "Surdulica");
        var city2 = new City("11000", "Beograd");


        var addresses = new List<Address>{
            new Address {id="1",number=14,PostalCode="17530", street="Milutina Stojanovica",City=city1},
            new Address {id="1",number=12,PostalCode="11000", street="Ustanicka",City=city2}
        };

        mockAddressRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(addresses);
        var AddressService = new AddressService(mockAddressRepo.Object, mockCityRepo.Object);

        var result = await AddressService.GetAddresses();
        Assert.Equal(2, ((List<Address>)result).Count);

    }

    [Fact]
    public async Task DeleteAddress_ShouldDeleteAddress_WhenAddressExists()
    {
        var city1 = new City("17530", "Surdulica");

        var address = new Address { id = "1", number = 14, PostalCode = "17530", street = "Milutina Stojanovica", City = city1 };

        var mockAddressRepo = new Mock<IGenericRepository<Address>>();
        var mockCityRepo = new Mock<IGenericRepository<City>>();

        mockAddressRepo.Setup(r => r.GetOneAsync(address.id)).ReturnsAsync(address);
        mockAddressRepo.Setup(r => r.DeleteAsync(address.id)).ReturnsAsync(true);
        var AddressService = new AddressService(mockAddressRepo.Object, mockCityRepo.Object);

        var result = await AddressService.DeleteAddress(address.id);

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAddress_ShouldUpdate_WhenCityExist_()
    {
        var city = new City("17530", "Surdulica");
        var address = new Address("1", 14, "Milutina Stojanovic", city, "17530");
        var mockCityRepo = new Mock<IGenericRepository<City>>();
        var mockAddressRepo = new Mock<IGenericRepository<Address>>();
        var addressToUpdate = new Address("1", 14, "Jugoslovenska", city, "17530");

        var addressService = new AddressService(mockAddressRepo.Object, mockCityRepo.Object);
        mockCityRepo.Setup(r => r.GetOneAsync("17530")).ReturnsAsync(city);
        mockAddressRepo.Setup(r => r.GetOneAsync("1")).ReturnsAsync(address);
        mockAddressRepo.Setup(r => r.UpdateAsync(addressToUpdate, "1"));

        var result =await addressService.UpdateAddress(addressToUpdate, "1", "17530");
        Assert.NotNull(result);
        Assert.Equal("Jugoslovenska", result.street);

    }

    [Fact]
public async Task CreateAddress_ShouldCreate_WhenCityExists()
{
    var mockAddressRepo = new Mock<IGenericRepository<Address>>();
    var mockCityRepo = new Mock<IGenericRepository<City>>();

    var city = new City("17530", "Surdulica");
    var newAddress = new Address("1", 12, "Milutina Stojanovica", city, "17530");

    mockCityRepo.Setup(r => r.GetOneAsync("17530")).ReturnsAsync(city);

    mockAddressRepo.Setup(r => r.CreateAsync(It.IsAny<Address>())).ReturnsAsync(newAddress);

    var addressService = new AddressService(mockAddressRepo.Object, mockCityRepo.Object);

    var result = await addressService.CreateAddress(newAddress.MapDomainEntityToDTO(), "17530");

    Assert.NotNull(result);
    Assert.Equal("Milutina Stojanovica", result.street);
    Assert.Equal("17530", result.PostalCode);
    Assert.Equal(city, result.City);
}

}