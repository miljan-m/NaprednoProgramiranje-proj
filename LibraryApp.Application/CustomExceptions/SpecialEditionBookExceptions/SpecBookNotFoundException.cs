namespace LibraryApp.Application.CustomExceptions.SpecialEditionBookExceptions;

public class SpecBookNotFoundException : NotFoundException
{
    public SpecBookNotFoundException(string isbn) : base($"Book with isbn = {isbn} doesn't exist") { }
}