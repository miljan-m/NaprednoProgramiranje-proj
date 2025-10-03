using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;


namespace LibraryApp.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> customerRepository;

    public CustomerService(IGenericRepository<Customer> customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<IEnumerable<GetCustomersDTO>> GetCustomers()
    {
        var customersList = await customerRepository.GetAllAsync();
        var customers=customersList.Select(c => c.MapDomainEntitiesToDTO()).ToList();
        if (customers == null) throw new NotFoundException("Database is empty");
        return customers;
    }

    public async Task<GetCustomerDTO> GetCustomer(string jmbg)
    {   
        if (jmbg.Length < 0 ||  jmbg.Length > 13) throw new CustomerInvalidArgumentException(jmbg);
        var customer = await customerRepository.GetOneAsync(jmbg);
        if (customer == null) throw new CustomerNotFoundException(jmbg);
        return customer.MapDomainEntityToDTO();
    }

    public async Task<bool> DeleteCustomer(string jmbg)
    {   
        if (jmbg.Length < 0 || jmbg.ToString().Length > 13) throw new CustomerInvalidArgumentException(jmbg);
        var customer = await customerRepository.GetOneAsync(jmbg);
        if (customer == null) throw new CustomerNotFoundException(jmbg);
        return await customerRepository.DeleteAsync(jmbg);
    }

    public async Task<UpdateCustomerDTO> UpdateCustomer(UpdateCustomerDTO updatedCustomer, string jmbg)
    {
        if (jmbg.Length < 0 || jmbg.ToString().Length > 13) throw new CustomerInvalidArgumentException(jmbg);
        var customer = await customerRepository.GetOneAsync(jmbg);
        if (customer == null) throw new CustomerNotFoundException(jmbg);
        await customerRepository.UpdateAsync(updatedCustomer.MapDtoToDomainEntity(customer), jmbg);
        return updatedCustomer;   
    }

    public async Task<Customer> CreateCustomer(CreateCustomerDTO customer)
    {
        var nonDtoCustomer = customer.MapDtoToDomainEntity();
        return await customerRepository.CreateAsync(nonDtoCustomer);
    }
}