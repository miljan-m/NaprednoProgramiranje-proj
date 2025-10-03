namespace LibraryApp.Mappers;

public static class ExtensionCustomerMethods
{
    public static GetCustomerDTO MapDomainEntityToDTO(this Customer customer)
    {
        return new GetCustomerDTO
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
        };
    }
    public static GetCustomersDTO MapDomainEntitiesToDTO(this Customer customer)
    {
        return new GetCustomersDTO
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
        };
    }
    public static Customer MapDtoToDomainEntity(this CreateCustomerDTO customerDTO)
    {
        return new Customer
        {   JMBG=customerDTO.JMBG,
            FirstName = customerDTO.FirstName,
            LastName = customerDTO.LastName,
        };
    }

    public static Customer MapDtoToDomainEntity(this UpdateCustomerDTO customerDTO, Customer customer)
    {
        customer.FirstName = customerDTO.FirstName;
        customer.LastName = customerDTO.LastName;
        return customer;
    }
}