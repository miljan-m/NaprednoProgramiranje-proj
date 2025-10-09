using LibraryApp.Application.Interfaces;
using System.Text.Json;

namespace LibraryApp.Application.Services.JSONServices;

public class JSONAuthorService<Author> : IJSONService<Author> where Author : class
{
    public async void WriteJSONInFile(Author obj)
    {
        var fileName = "AuthorJsonFile.json";
        using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, obj);
    }
}