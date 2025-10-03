namespace LibraryApp.Application.CustomExceptions.AdminExceptions;

public class AdminNotFoundException : NotFoundException
{
    public AdminNotFoundException(string adminId) : base($"Author with id = {adminId} doesn't exist") { }
}