using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;


namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja poslovnom logikom za entitet <see cref="Customer"/>.
/// Omogućava kreiranje, vraćanje, ažuriranje i brisanje kupaca.
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> customerRepository;
    /// <summary>
    /// Inicijalizuje novi <see cref="CustomerService"/> sa prosleđenim repozitorijumom za kupce
    /// </summary>
    /// <param name="customerRepository">Generički repozitorijum za entitet <see cref="Customer"/></param>
    public CustomerService(IGenericRepository<Customer> customerRepository)
    {
        this.customerRepository = customerRepository;
    }
    /// <summary>
    /// Vraća sve kupce iz baze
    /// </summary>
    /// <returns>Kolekciju DTO objekata tipa <see cref="GetCustomersDTO"/> koji predstavljaju sve kupce</returns>
    /// <exception cref="NotFoundException">Baca se ako baza ne sadrži nijednog kupca</exception>
    public async Task<IEnumerable<GetCustomersDTO>> GetCustomers()
    {
        var customersList = await customerRepository.GetAllAsync();
        var customers=customersList.Select(c => c.MapDomainEntitiesToDTO()).ToList();
        if (customers == null) throw new NotFoundException("Database is empty");
        return customers;
    }
    /// <summary>
    /// Vraća jednog kupca u zavisnosti od prolsedjenog JMBG
    /// </summary>
    /// <param name="jmbg">JMBG kupca</param>
    /// <returns>DTO objekat tipa <see cref="GetCustomerDTO"/> sa podacima o kupcu</returns>
    /// <exception cref="CustomerInvalidArgumentException">Baca se ako JMBG nije validan</exception>
    /// <exception cref="CustomerNotFoundException">Baca se ako kupac sa datim JMBG-om ne postoji</exception>
    public async Task<GetCustomerDTO> GetCustomer(string jmbg)
    {   
        if (jmbg.Length < 0 ||  jmbg.Length > 13) throw new CustomerInvalidArgumentException(jmbg);
        var customer = await customerRepository.GetOneAsync(jmbg);
        if (customer == null) throw new CustomerNotFoundException(jmbg);
        return customer.MapDomainEntityToDTO();
    }
    /// <summary>
    /// Briše kupca u zavisnosti od JMBG
    /// </summary>
    /// <param name="jmbg">JMBG kupca koji se briše</param>
    /// <returns>Vraća TRUE ako je brisanje uspešno</returns>
    /// <exception cref="CustomerInvalidArgumentException">Baca se ako JMBG nije validan</exception>
    /// <exception cref="CustomerNotFoundException">Baca se ako kupac sa datim JMBG-om ne postoji</exception>
    public async Task<bool> DeleteCustomer(string jmbg)
    {   
        if (jmbg.Length < 0 || jmbg.ToString().Length > 13) throw new CustomerInvalidArgumentException(jmbg);
        var customer = await customerRepository.GetOneAsync(jmbg);
        if (customer == null) throw new CustomerNotFoundException(jmbg);
        return await customerRepository.DeleteAsync(jmbg);
    }
    /// <summary>
    /// Ažurira podatke o kupcu
    /// </summary>
    /// <param name="updatedCustomer">DTO objekat tipa <see cref="UpdateCustomerDTO"/> sa novim podacima o kupcu</param>
    /// <param name="jmbg">Jedinstveni JMBG kupca koji se ažurira</param>
    /// <returns>DTO objekat tipa <see cref="UpdateCustomerDTO"/> sa ažuriranim podacima o kupcu</returns>
    /// <exception cref="CustomerInvalidArgumentException">Baca se ako JMBG nije validan</exception>
    /// <exception cref="CustomerNotFoundException">Baca se ako kupac sa datim JMBG-om ne postoji</exception>
    public async Task<UpdateCustomerDTO> UpdateCustomer(UpdateCustomerDTO updatedCustomer, string jmbg)
    {
        if (jmbg.Length < 0 || jmbg.ToString().Length > 13) throw new CustomerInvalidArgumentException(jmbg);
        var customer = await customerRepository.GetOneAsync(jmbg);
        if (customer == null) throw new CustomerNotFoundException(jmbg);
        await customerRepository.UpdateAsync(updatedCustomer.MapDtoToDomainEntity(customer), jmbg);
        return updatedCustomer;   
    }
    /// <summary>
    /// Kreira novog kupca
    /// </summary>
    /// <param name="customer">DTO objekat tipa <see cref="CreateCustomerDTO"/> sa podacima o novom kupcu</param>
    /// <returns>Instancu kreiranog kupca tipa <see cref="Customer"/></returns>
    public async Task<Customer> CreateCustomer(CreateCustomerDTO customer)
    {
        var nonDtoCustomer = customer.MapDtoToDomainEntity();
        return await customerRepository.CreateAsync(nonDtoCustomer);
    }
}