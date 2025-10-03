


namespace LibraryApp.Application.Interfaces;

public interface ICityService
{
    public Task<IEnumerable<City>> GetCities();
    public Task<City> GetCity(string postalCode);
    public Task<bool> DeleteCity(string postalCode);
    public Task<City> UpdateCity(string postalCode, City cityToUpdate);
    public Task<City> CreateCity(City city);
}