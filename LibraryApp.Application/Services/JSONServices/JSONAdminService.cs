using LibraryApp.Application.Interfaces;
using System.Text.Json;

namespace LibraryApp.Application.Services.JSONServices;

public class JSONAdminService<Admin> : IJSONService<Admin> where Admin : class
{
    public async void WriteJSONInFile(Admin obj)
    {
        var fileName = "AdminJsonFile.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, obj);
    }
}