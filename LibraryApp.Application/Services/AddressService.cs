using LibraryApp.Application.DTOs.RequestDTO.Address;
using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;

namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja logikom vezanom za adrese u aplikaciji.
/// Omogućava kreiranje, ažuriranje, brisanje i preuzimanje adresa.
/// </summary>

public class AddressService : IAddressService
{



    private readonly IGenericRepository<Address> addressRepository;
    private readonly IGenericRepository<City> cityRepository;
    /// <summary>
    /// Inicijalizuje novi <see cref="AddressService"/> sa prosleđenim repozitorijumima za adrese i gradove.
    /// </summary>
    /// <param name="addressRepository">Generički repozitorijum za entitet <see cref="Address"/>.</param>
    /// <param name="cityRepository">Generički repozitorijum za entitet <see cref="City"/>.</param>
    public AddressService(IGenericRepository<Address> addressRepository, IGenericRepository<City> cityRepository)
    {

        this.addressRepository = addressRepository;
        this.cityRepository = cityRepository;
    }
    /// <summary>
    /// Kreira novu adresu i povezuje je sa gradom prema poštanskom broju.
    /// </summary>
    /// <param name="address">DTO objekat koji sadrži podatke o adresi.</param>
    /// <param name="postalcode">Poštanski broj grada sa kojim se adresa povezuje.</param>
    /// <returns>Vraća kreiranu adresu</returns>
    /// <exception cref="Exception">Ako grad sa datim poštanskim brojem ne postoji.</exception>
    public async Task<Address> CreateAddress(AddressCreateDTO address, string postalcode)
    {
        var city = await cityRepository.GetOneAsync(postalcode);
        var newAddress = address.MapDTOToDomainEntity(city);
        await addressRepository.CreateAsync(newAddress);
        return newAddress;
    }
    /// <summary>
    /// Briše adresu na osnovu njenog identifikatora.
    /// </summary>
    /// <param name="id">Identifikator adrese koja se briše.</param>
    /// <returns>Vraća TRUE ako je brisanje uspešno.</returns>
    /// <exception cref="Exception">Ako adresa ne postoji.</exception>
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
    /// <summary>
    /// Vraca sve adrese iz sistema.
    /// </summary>
    /// <returns>Kao povratnu vrednost vraca sve adrese iz baze podataka</returns>
    public async Task<IEnumerable<Address>> GetAddresses()
    {
        var addresses = await addressRepository.GetAllAsync();
        return addresses;
    }
    /// <summary>
    /// Vraca adresu na osnovu id-a
    /// </summary>
    /// <param name="id">Identifikator adrese.</param>
    /// <returns>Pronađena adresa ili NULL ako ne postoji.</returns>
    public async Task<Address> GetAddress(string id)
    {
        var address = await addressRepository.GetOneAsync(id);
        return address;
    }
    /// <summary>
    /// Ažurira adresu na osnovupoštanskog broja grada.
    /// </summary>
    /// <param name="addressToUpdate">Adresa sa novim vrednostima</param>
    /// <param name="id">Identifikator adrese koja se ažurira</param>
    /// <param name="postalcode">Poštanski broj grada sa kojim se adresa povezuje</param>
    /// <returns>Ažurirana instanca <see cref="Address"/>.</returns>
    /// <exception cref="Exception">Ako grad sa datim poštanskim brojem ne postoji.</exception>
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