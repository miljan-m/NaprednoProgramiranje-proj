using LibraryApp.Mappers;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.DTOs.ResponseDTO.Customer;
using LibraryApp.Application.DTOs.RequestDTO.Customer;
using FluentValidation;
using LibraryApp.Domen.Models;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService customerService;
    private readonly IValidator<Customer> customerValidator;
    public CustomerController(ICustomerService customerService, IValidator<Customer> customerValidator)
    {
        this.customerService = customerService;
        this.customerValidator = customerValidator;
    }

    [HttpGet]
    [EndpointSummary("Get all existing customers")]
    [EndpointDescription("This endpoint returns list of all customers. Informations about JMBG are excluded")]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await customerService.GetCustomers();
        return Ok(customers);
    }

    [HttpGet("{jmbg}")]
    [EndpointSummary("Get one customers")]
    [EndpointDescription("This endpoint returns one customer based on provided jmbg")]
    public async Task<ActionResult<GetCustomerDTO>> GetCustomer([FromRoute] string jmbg)
    {
        var customer = await customerService.GetCustomer(jmbg);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpDelete("{jmbg}")]
    [EndpointSummary("Removing customer")]
    [EndpointDescription("This endpoint deletes one customer based on provided jmbg")]
    public async Task<ActionResult> DeleteCustomer([FromRoute] string jmbg)
    {
        var isDeleted = await customerService.DeleteCustomer(jmbg);
        if (isDeleted) return NoContent();
        return NotFound();
    }

    [HttpPost]
    [EndpointSummary("Creating customer")]
    [EndpointDescription("This endpoint creates new customer based on information that has been provided in body of request")]
    public async Task<ActionResult<CreateCustomerDTO>> CreateCustomer([FromBody] CreateCustomerDTO customerToCreate)
    {
        var customerToValidate = customerToCreate.MapDtoToDomainEntity();
        var validateResult =await customerValidator.ValidateAsync(customerToValidate);
        if (!validateResult.IsValid)
        {
            
             foreach (var fail in validateResult.Errors){
                    ModelState.AddModelError(fail.PropertyName, fail.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }
        var createdCustomer = await customerService.CreateCustomer(customerToCreate);
        var createdCustomerDto = createdCustomer.MapDomainEntityToDTO();
        return CreatedAtAction(nameof(GetCustomer),new{jmbg=createdCustomer.JMBG}, createdCustomerDto);
    }

    [HttpPut("{jmbg}")]
    [EndpointSummary("Updating customer")]
    [EndpointDescription("This endpoint updates customer based on information that has been provided in body of request")]
    public async Task<ActionResult<UpdateCustomerDTO>> UpdateCustomer([FromRoute]string jmbg,[FromBody] UpdateCustomerDTO updatedCustomerDTO)
    {
        var customer = await customerService.UpdateCustomer(updatedCustomerDTO, jmbg);
        return Ok(customer);
    }
}