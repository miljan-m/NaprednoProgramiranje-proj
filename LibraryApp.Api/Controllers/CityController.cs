
using FluentValidation;
using LibraryApp.Application.Interfaces;
using LibraryApp.Domen.Models;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("cities")]
public class CityController : ControllerBase
{

    private readonly ICityService cityService;
    private readonly IValidator<City> cityValidator;
    public CityController(ICityService cityService, IValidator<City> cityValidator)
    {
        this.cityService = cityService;
        this.cityValidator = cityValidator;
    }


[HttpGet]
    [EndpointSummary("Gets all existing cities")]
    [EndpointDescription("This endpoint returns all cities")]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        var cities = await cityService.GetCities();
        return Ok(cities);
    }

    [HttpGet("{postalcode}")]
    [EndpointSummary("Gets one city")]
    [EndpointDescription("This endpoint returns one city based on provided postal code")]
    public async Task<ActionResult<City>> GetAuthor([FromRoute] string postalcode)
    {
        var city = await cityService.GetCity(postalcode);
        if (city == null) return NotFound();
        return Ok(city);
    }

    [HttpPost]
    [EndpointSummary("Creation of new author")]
    [EndpointDescription("This endpoint creates new author based on information that has been provided in body of request")]
    public async Task<ActionResult<City>> CreateCity([FromBody] City cityToCreate)
    {
        var validateResult = await cityValidator.ValidateAsync(cityToCreate);
        
        if (!validateResult.IsValid)
        {
            
             foreach (var fail in validateResult.Errors){
                    ModelState.AddModelError(fail.PropertyName, fail.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }
        var city = await cityService.CreateCity(cityToCreate);
        return Ok(city);
    }

    [HttpDelete("{postalcode}")]
    [EndpointSummary("Removing city")]
    [EndpointDescription("This endpoint deletes city based on provided postal code")]
    public async Task<ActionResult> DeleteAuthor([FromRoute] string posatlcode)
    {
        var isDeleted = await cityService.DeleteCity(posatlcode);
        if (isDeleted == false) return NotFound();
        return NoContent();
    }

    [HttpPut("{postalcode}")]
    [EndpointSummary("Updating city")]
    [EndpointDescription("This endpoint updates city based on information that has been provided in body of request")]
    public async Task<ActionResult<City>> UpdateAuthor([FromRoute] string postalcode, [FromBody] City updatedCity)
    {
        var city = await cityService.UpdateCity(postalcode, updatedCity);
        if (city == null) return NotFound();
        return Ok(city);
    }
}