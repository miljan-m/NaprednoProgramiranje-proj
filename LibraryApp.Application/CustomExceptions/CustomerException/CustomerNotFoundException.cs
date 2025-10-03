namespace LibraryApp.Application.CustomExceptions.CustomerException;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException(string jmbg) : base($"Customer with jmbg = {jmbg} doesn't exist") { }
}