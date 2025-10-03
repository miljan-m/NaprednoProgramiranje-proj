namespace LibraryApp.Application.CustomExceptions.AuthorExceptions;

public class AuthorInvalidArgumentException : InvalidArgumentException
{
    public AuthorInvalidArgumentException(string authorId):base($"Given value of id: {authorId} is not valid") { }

}