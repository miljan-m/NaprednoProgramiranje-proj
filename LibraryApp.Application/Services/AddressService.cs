using LibraryApp.Application.DTOs.RequestDTO.Address;
using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;

namespace LibraryApp.Application.Services;


public class AddressService : IAddressService
{

    private readonly IGenericRepository<Address> addressRepository;
    private readonly IGenericRepository<City> cityRepository;

    public AddressService(IGenericRepository<Address> addressRepository, IGenericRepository<City> cityRepository)
    {

        this.addressRepository = addressRepository;
        this.cityRepository = cityRepository;
    }

    public async Task<Address> CreateAddress(AddressCreateDTO address,string postalcode)
    {
        var city =await cityRepository.GetOneAsync(postalcode);
        var newAddress = address.MapDTOToDomainEntity(city);
        await addressRepository.CreateAsync(newAddress);
        return newAddress;
    }

    public async Task<bool> DeleteAddress(string id)
    {
        var address = await addressRepository.GetOneAsync(id);
        if (address == null)
        {
            throw new Exception("Address does not exist!");
        }
        await addressRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<Address>> GetAddresses()
    {
        var addresses = await addressRepository.GetAllAsync();
        return addresses;
    }

    public async Task<Address> GetAddress(string id)
    {
        var address = await addressRepository.GetOneAsync(id);
        return address;
    }

    public async Task<Address> UpdateAddress(Address addressToUpdate, string id, string postalcode)
    {
        var address = await addressRepository.GetOneAsync(id);
        var city = await cityRepository.GetOneAsync(postalcode);
        if (city == null) throw new Exception("City does not exist");
        addressToUpdate.id = id;
        await addressRepository.UpdateAsync(addressToUpdate, id);
        return addressToUpdate;
    }

}