using LibraryApp.Application.Interfaces;
using LibraryApp.Application.DTOs.ResponseDTO.SpecialEditionBook;
using LibraryApp.Application.DTOs.RequestDTO.SpecialEditionBook;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("/specialbook")]
public class SpecialEditionBookController : ControllerBase
{
    private readonly ISpecialEditionBookService bookService;

    public SpecialEditionBookController(ISpecialEditionBookService bookService)
    {
        this.bookService = bookService;
    }

    [HttpGet]
    [EndpointSummary("Gets all existing special edition books")]
    [EndpointDescription("This endpoint return list of all special edition books")]
    public async Task<ActionResult<IEnumerable<GetSpecialBooksDTO>>> GetSpecialBooks()
    {
        var specialBooks = await bookService.GetBooks();
        return Ok(specialBooks);
    }

    [HttpGet("{isbn}")]
    [EndpointSummary("Gets one special edition book")]
    [EndpointDescription("This endpoint return one special edition books based on provided isbn")]
    public async Task<ActionResult<GetSpecialBookDTO>> GetSpecialBook([FromRoute] string isbn)
    {
        var book = await bookService.GetBook(isbn);
        return Ok(book);
    }

    [HttpDelete("{isbn}")]
    [EndpointSummary("Removing special edition book")]
    [EndpointDescription("This endpoint deletes special edition books based on provided isbn")]
    public async Task<ActionResult> DeleteSpecialBook(string isbn)
    {
        var isDeleted = await bookService.DeleteBook(isbn);
        if (isDeleted == false) return NotFound();
        return Ok();
    }

    [HttpPost("{authorId}")]
    [EndpointSummary("Creating special edition book")]
    [EndpointDescription("This endpoint creates new special edition books based on information that has been provided in body of request")]
    public async Task<ActionResult<CreateSpecialBookDTO>> CreateSpecialBook([FromRoute] string authorId, CreateSpecialBookDTO specialEditionBook)
    {
        var book = await bookService.CreateBook(specialEditionBook, authorId);
        return Ok(book);
    }

    [HttpPut("{isbn}")]
    [EndpointSummary("Updating special edition book")]
    [EndpointDescription("This endpoint updtaes new special edition books based on information that has been provided in body of request")]
    public async Task<ActionResult<UpdateSpecialBookDTO>> UpdateSpecialBook([FromRoute] string isbn, [FromBody] UpdateSpecialBookDTO updatedBook)
    {
        var specialBookDto = await bookService.UpdateBook(isbn, updatedBook);
        if (specialBookDto == null) return NotFound();
        return Ok(specialBookDto);
    }

}