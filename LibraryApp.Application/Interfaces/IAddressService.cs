using LibraryApp.Application.DTOs.RequestDTO.Address;

namespace LibraryApp.Application.Interfaces;

public interface IAddressService
{
    public Task<IEnumerable<Address>> GetAddresses();
    public Task<Address> GetAddress(string id);
    public Task<bool> DeleteAddress(string id);
    public Task<Address> UpdateAddress(Address addressToUpdate, string id, string postalcode);
    public Task<Address> CreateAddress(AddressCreateDTO address,string postalcode);
}