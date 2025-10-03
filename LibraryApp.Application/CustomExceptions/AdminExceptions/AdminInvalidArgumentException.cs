namespace LibraryApp.Application.CustomExceptions.AdminExceptions;

public class AdminInvalidArgumentException : InvalidArgumentException
{
    public AdminInvalidArgumentException(string adminId) : base($"Given value of admin id: {adminId} is not valid") { }
}