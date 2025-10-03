namespace LibraryApp.Application.CustomExceptions.BookExceptions;

public class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(string isbn) : base($"Book with isbn = {isbn} doesn't exist") { }
}