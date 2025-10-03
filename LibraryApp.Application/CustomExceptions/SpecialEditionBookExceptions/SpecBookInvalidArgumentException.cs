namespace LibraryApp.Application.CustomExceptions.SpecialEditionBookExceptions;

public class SpecBookInvalidArgumentException : InvalidArgumentException
{
    public SpecBookInvalidArgumentException(string isbn) : base($"Given value of id: {isbn} is not valid") { }
}