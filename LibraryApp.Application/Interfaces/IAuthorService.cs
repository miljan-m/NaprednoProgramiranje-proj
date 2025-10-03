namespace LibraryApp.Application.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<GetAuthorsDTO>> GetAuthors();
    public Task<Author> GetAuthor(string authorId);
    public Task<bool> DeleteAuthor(string authorId);
    public Task<Author> CreateAuthor(AuthorCreateDTO authorDto);
    public Task<GetAuthorDTO> UpdateAuthor(string authorId, AuthorUpdateDTO updatedAuthor);
}