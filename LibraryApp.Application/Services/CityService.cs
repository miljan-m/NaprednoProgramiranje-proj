using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;
using System.Runtime.CompilerServices;

namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja poslovnom logikom za entitet <see cref="City"/>.
/// Omogućava kreiranje, preuzimanje, ažuriranje i brisanje gradova.
/// </summary>
public class CityService : ICityService
{
    
    private readonly IGenericRepository<City> cityRepository;
    /// <summary>
    /// Inicijalizuje novi <see cref="CityService"/> sa prosleđenim repozitorijumom za gradove
    /// </summary>
    /// <param name="cityRepository">Generički repozitorijum za entitet <see cref="City"/></param>
    public CityService(IGenericRepository<City> cityRepository)
    {

        this.cityRepository = cityRepository;
    }
    /// <summary>
    /// Kreira novi grad u bazi podataka
    /// </summary>
    /// <param name="city">Objekat tipa <see cref="City"/> koji sadrži podatke o novom gradu</param>
    /// <returns>Instanca kreiranog grada tipa <see cref="City"/></returns>
    public async Task<City> CreateCity(City city)
    {
        await cityRepository.CreateAsync(city);
        return city;
    }
    /// <summary>
    /// Briše grad iz baze podataka
    /// </summary>
    /// <param name="postalCode">Poštanski broj grada koji se briše</param>
    /// <returns>Vraća TRUE ako je brisanje uspešno</returns>
    public async Task<bool> DeleteCity(string postalCode)
    {
        await cityRepository.DeleteAsync(postalCode);
        return true;
    }
    /// <summary>
    /// Vraća sve gradove iz baze podataka
    /// </summary>
    /// <returns>Kolekcija objekata tipa <see cref="City"/> koja predstavlja sve gradove</returns>
    public async Task<IEnumerable<City>> GetCities()
    {
        var cities = await cityRepository.GetAllAsync();
        return cities;
    }
    /// <summary>
    /// Vraća grad prema poštanskom broju
    /// </summary>
    /// <param name="postalCode">Poštanski broj grada koji se vraca</param>
    /// <returns>Objekat tipa <see cref="City"/> sa podacima o traženom gradu</returns>
    public async Task<City> GetCity(string postalCode)
    {
        var city = await cityRepository.GetOneAsync(postalCode);
        return city;
    }
    /// <summary>
    /// Ažurira podatke o postojećem gradu
    /// </summary>
    /// <param name="postalCode">Poštanski broj grada koji se ažurira</param>
    /// <param name="cityToUpdate">Objekat tipa <see cref="City"/> koji sadrži nove podatke o gradu</param>
    /// <returns>Objekat tipa <see cref="City"/> sa ažuriranim podacima o gradu</returns>
    /// <exception cref="Exception">Baca se ako grad sa datim poštanskim brojem ne postoji</exception>
    public async Task<City> UpdateCity(string postalCode, City cityToUpdate)
    {
        var city = await cityRepository.GetOneAsync(postalCode);
        if (city == null) throw new Exception("City does not exist");
        cityToUpdate.PostalCode = postalCode;
        await cityRepository.UpdateAsync(cityToUpdate, postalCode);
        return cityToUpdate;
    }
}