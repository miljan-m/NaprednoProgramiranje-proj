using LibraryApp.Application.Interfaces;
using System.Text.Json;

namespace LibraryApp.Application.Services.JSONServices;

public class JSONCityService<City> : IJSONService<City> where City : class
{
    public async void WriteJSONInFile(City obj)
    {
        var fileName = "CityJsonFile.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, obj);
    }
}