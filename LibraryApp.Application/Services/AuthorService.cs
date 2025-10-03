using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Mappers;

namespace LibraryApp.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IGenericRepository<Author> authorRepository;

    public AuthorService(IGenericRepository<Author> authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<IEnumerable<GetAuthorsDTO>> GetAuthors()
    {
        var authors = await authorRepository.GetAllAsync(); 
        var authorsDto = authors.Select(a => a.MapDomainEntitiesToDto());
        if (authors == null) throw new NotFoundException("Database is empty");
        return authorsDto;
    }

    public async Task<Author> GetAuthor(string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        return author;
    }

    public async Task<bool> DeleteAuthor(string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        return await authorRepository.DeleteAsync(authorId);
    }

    public async Task<Author> CreateAuthor(AuthorCreateDTO authorDto)
    {
        var author = authorDto.MapDtoToDomainEntity();
        await authorRepository.CreateAsync(author);
        return author;
    }

    public async Task<GetAuthorDTO> UpdateAuthor(string authorId, AuthorUpdateDTO updatedAuthor)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        await authorRepository.UpdateAsync(updatedAuthor.MapDtoToDomainEntity(author), authorId);
        return author.MapDomainEntityToDto();
    }
}