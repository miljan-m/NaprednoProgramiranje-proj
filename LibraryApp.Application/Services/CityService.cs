using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;
using System.Runtime.CompilerServices;

namespace LibraryApp.Application.Services;

public class CityService : ICityService
{
    
    private readonly IGenericRepository<City> cityRepository;
    public CityService(IGenericRepository<City> cityRepository)
    {
        
        this.cityRepository = cityRepository;
    }




    public async Task<City> CreateCity(City city)
    {
        await cityRepository.CreateAsync(city);
        return city;
    }

    public async Task<bool> DeleteCity(string postalCode)
    {
        await cityRepository.DeleteAsync(postalCode);
        return true;
    }

    public async Task<IEnumerable<City>> GetCities()
    {
        var cities = await cityRepository.GetAllAsync();
        return cities;
    }

    public async Task<City> GetCity(string postalCode)
    {
        var city = await cityRepository.GetOneAsync(postalCode);
        return city;
    }

    public async Task<City> UpdateCity(string postalCode, City cityToUpdate)
    {
        var city = await cityRepository.GetOneAsync(postalCode);
        if (city == null) throw new Exception("City does not exist");
        cityToUpdate.PostalCode = postalCode;
        await cityRepository.UpdateAsync(cityToUpdate, postalCode);
        return cityToUpdate;
    }
}