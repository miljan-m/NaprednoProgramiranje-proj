namespace LibraryApp.Application.CustomExceptions.BookExceptions;

public class BookInvalidArgumentException : InvalidArgumentException
{
    public BookInvalidArgumentException(string isbn) : base($"Given value of id: {isbn} is not valid") { }
}