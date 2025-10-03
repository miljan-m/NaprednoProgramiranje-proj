namespace LibraryApp.Application.CustomExceptions.CustomerException;

public class CustomerInvalidArgumentException : InvalidArgumentException
{
    public CustomerInvalidArgumentException(string jmbg) : base($"Given value of id: {jmbg} is not valid") { }
}