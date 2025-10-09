using LibraryApp.Application.Interfaces;
using System.Text.Json;

namespace LibraryApp.Application.Services.JSONServices;

public class JSONBookService<Book> : IJSONService<Book> where Book : class
{
    public async void WriteJSONInFile(Book obj)
    {
        var fileName = "BookJsonFile.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, obj);
    }
}