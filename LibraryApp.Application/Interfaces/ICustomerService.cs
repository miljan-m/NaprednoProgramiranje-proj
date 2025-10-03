namespace LibraryApp.Application.Interfaces;

public interface ICustomerService
{
    public Task<IEnumerable<GetCustomersDTO>> GetCustomers();
    public Task<GetCustomerDTO> GetCustomer(string jmbg);
    public Task<bool> DeleteCustomer(string jmbg);
    public Task<UpdateCustomerDTO> UpdateCustomer(UpdateCustomerDTO updatedCustomer,string jmbg);
    public Task<Customer> CreateCustomer(CreateCustomerDTO customer);
}