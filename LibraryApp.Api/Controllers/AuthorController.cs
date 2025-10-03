using LibraryApp.Application.Interfaces;
using LibraryApp.Application.DTOs.RequestDTO.Author;
using LibraryApp.Application.DTOs.ResponseDTO.Authors;
using LibraryApp.Domen.Models;
using FluentValidation;
using LibraryApp.Application.Mappers;

namespace LibraryApp.Api.Controllers;

[ApiController]



[Route("authors")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService authorService;
     private readonly IValidator<Author> authorValidator;

    public AuthorController(IAuthorService authorService,IValidator<Author> authorValidator)
    {
        this.authorService = authorService;
        this.authorValidator = authorValidator;
    }

    [HttpGet]
    [EndpointSummary("Gets all existing authors")]
    [EndpointDescription("This endpoint returns all authors")]
    public async Task<ActionResult<IEnumerable<GetAuthorsDTO>>> GetAuthors()
    {
        var authors = await authorService.GetAuthors();
        return Ok(authors);
    }

    [HttpGet("{authorId}")]
    [EndpointSummary("Gets one author")]
    [EndpointDescription("This endpoint returns one author based on provided id")]
    public async Task<ActionResult<GetAuthorDTO>> GetAuthor([FromRoute] string authorId)
    {
        var author = await authorService.GetAuthor(authorId);
        if (author == null) return NotFound();
        return Ok(author);
    }

    [HttpPost]
    [EndpointSummary("Creation of new author")]
    [EndpointDescription("This endpoint creates new author based on information that has been provided in body of request")]
    public async Task<ActionResult<GetAuthorDTO>> CreateAuthor([FromBody] AuthorCreateDTO authorCreate)
    {   var authorToValidate = authorCreate.MapDtoToDomainEntity();
        var validateResult =await authorValidator.ValidateAsync(authorToValidate);
        if (!validateResult.IsValid)
        {
            
             foreach (var fail in validateResult.Errors){
                    ModelState.AddModelError(fail.PropertyName, fail.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }
        var author = await authorService.CreateAuthor(authorCreate);
        return Ok(author);
    }

    [HttpDelete("{authorId}")]
    [EndpointSummary("Removing author")]
    [EndpointDescription("This endpoint deletes author based on provided id")]
    public async Task<ActionResult> DeleteAuthor([FromRoute] string authorId)
    {
        var isDeleted = await authorService.DeleteAuthor(authorId);
        if (isDeleted == false) return NotFound();
        return NoContent();
    }

    [HttpPut("{authorId}")]
    [EndpointSummary("Updating author")]
    [EndpointDescription("This endpoint updates author based on information that has been provided in body of request")]
    public async Task<ActionResult<GetAuthorDTO>> UpdateAuthor([FromRoute] string authorId, [FromBody] AuthorUpdateDTO updatedAuthor)
    {
        var author = await authorService.UpdateAuthor(authorId, updatedAuthor);
        if (author == null) return NotFound();
        return Ok(author);
    }
}