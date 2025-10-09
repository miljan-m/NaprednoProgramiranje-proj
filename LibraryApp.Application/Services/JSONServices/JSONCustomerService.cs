using LibraryApp.Application.Interfaces;
using System.Text.Json;

namespace LibraryApp.Application.Services.JSONServices;

public class JSONCustomerService<Customer> : IJSONService<Customer> where Customer : class
{
    public async void WriteJSONInFile(Customer obj)
    {
        var fileName = "CustomerJsonFile.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, obj);
    }
}