namespace LibraryApp.Application.CustomExceptions.AuthorExceptions;

public class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException(string authorId):base($"Author with id = {authorId} doesn't exist") { }

}