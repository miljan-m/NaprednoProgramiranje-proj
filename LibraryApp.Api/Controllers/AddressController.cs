
using FluentValidation;
using LibraryApp.Application.DTOs.RequestDTO.Address;
using LibraryApp.Application.Interfaces;
using LibraryApp.Domen.Models;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("addresses")]
public class AddressController : ControllerBase
{
    private readonly IAddressService addressService;
    private readonly IValidator<AddressCreateDTO> addressValidator;
    public AddressController(IAddressService addressService,IValidator<AddressCreateDTO> addressValidator)
    {
        this.addressService = addressService;
        this.addressValidator = addressValidator;
    }


[HttpGet]
    [EndpointSummary("Gets all existing addresses")]
    [EndpointDescription("This endpoint returns all addresses")]
    public async Task<ActionResult<IEnumerable<City>>> GetAddresses()
    {
        var addresses = await addressService.GetAddresses();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Gets one address")]
    [EndpointDescription("This endpoint returns one address based on provided id")]
    public async Task<ActionResult<Address>> GetAddress([FromRoute] string id)
    {
        var address = await addressService.GetAddress(id);
        if (address == null) return NotFound();
        return Ok(address);
    }

    [HttpPost("{postalcode}")]
    [EndpointSummary("Creation of new address")]
    [EndpointDescription("This endpoint creates new address based on information that has been provided in body of request")]
    public async Task<ActionResult<Address>> CreateAddress([FromBody] AddressCreateDTO addressToCreate,[FromRoute] string postalcode)
    {
        var validateResult = await addressValidator.ValidateAsync(addressToCreate);
        
        if (!validateResult.IsValid)
        {
            
             foreach (var fail in validateResult.Errors){
                    ModelState.AddModelError(fail.PropertyName, fail.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }
        var address = await addressService.CreateAddress(addressToCreate,postalcode);
        return Ok(address);
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Removing address")]
    [EndpointDescription("This endpoint deletes address based on provided id")]
    public async Task<ActionResult> DeleteAddress([FromRoute] string id)
    {
        var isDeleted = await addressService.DeleteAddress(id);
        if (isDeleted == false) return NotFound();
        return NoContent();
    }

    [HttpPut("{id}")]
    [EndpointSummary("Updating address")]
    [EndpointDescription("This endpoint updates address based on information that has been provided in body of request")]
    public async Task<ActionResult<Address>> UpdateAddress([FromRoute] string id, [FromBody] Address updatedAddress,[FromQuery] string postalcode)
    {
        var city = await addressService.UpdateAddress(updatedAddress,id,postalcode);
        if (city == null) return NotFound();
        return Ok(city);
    }
}