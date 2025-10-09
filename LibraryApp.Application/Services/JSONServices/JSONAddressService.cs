using System.Text.Json;
using LibraryApp.Application.Interfaces;

namespace LibraryApp.Application.Services.JSONServices;

public class JSONAddressService<Address> : IJSONService<Address> where Address : class
{
    public async void WriteJSONInFile(Address obj)
    {
        var fileName = "AddressJsonFile.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, obj);
    }
}