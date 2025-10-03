namespace LibraryApp.Application.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<GetBooksDTO>> GetBooks();
    public Task<GetBookDTO> GetBook(string isbn);
    public Task<bool> DeleteBook(string isbn);
    public Task<GetBookDTO> UpdateBook(string isbn, BookUpdateDTO updatedBook);
    public Task<GetBookDTO> CreateBook(BookCreateDTO bookCreateDTO, string authorId);
}