using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Mappers;

namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja poslovnom logikom za entitet Author
/// Omogućava kreiranje, preuzimanje, ažuriranje i brisanje autora iz baze podataka.
/// </summary>
public class AuthorService : IAuthorService
{
    private readonly IGenericRepository<Author> authorRepository;
    /// <summary>
    /// Inicijalizuje novi <see cref="AuthorService"/> sa prosleđenim repozitorijumom za autore
    /// </summary>
    /// <param name="authorRepository">Generički repozitorijum za entitet <see cref="Author"/>.</param>
    public AuthorService(IGenericRepository<Author> authorRepository)
    {
        this.authorRepository = authorRepository;
    }
    /// <summary>
    /// Vraca sve autore iz baze 
    /// </summary>
    /// <returns>Kolekcija DTO objekata za autora koja sadrži informacije o svim autorima</returns>
    /// <exception cref="NotFoundException">Baca se ako baza ne sadrži nijednog autora</exception>
    public async Task<IEnumerable<GetAuthorsDTO>> GetAuthors()
    {
        var authors = await authorRepository.GetAllAsync(); 
        var authorsDto = authors.Select(a => a.MapDomainEntitiesToDto());
        if (authors == null) throw new NotFoundException("Database is empty");
        return authorsDto;
    }
    /// <summary>
    /// Vraca jednog autora na osnovu njegovog jedinstvenog identifikatora
    /// </summary>
    /// <param name="authorId">Jedinstveni identifikator autora</param>
    /// <returns>Objekat tipa <see cref="Author"/> koji predstavlja pronađenog autora</returns>
    /// <exception cref="AuthorNotFoundException"> Baca se ako autor sa zadatim authorId ne postoji.</exception>
    public async Task<Author> GetAuthor(string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        return author;
    }
    /// <summary>
    /// Briše autora iz baze 
    /// </summary>
    /// <param name="authorId">Jedinstveni identifikator autora</param>
    /// <returns>Vraća TRUE ako je autor uspešno obrisan.</returns>
    /// <exception cref="AuthorNotFoundException">Baca se ako autor sa zadatim authorId ne postoji.</exception>
    public async Task<bool> DeleteAuthor(string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        return await authorRepository.DeleteAsync(authorId);
    }
    /// <summary>
    /// Kreira novog autora na osnovu prosleđenog DTO-a
    /// </summary>
    /// <param name="authorDto">DTO objekat koji sadrži podatke o novom autoru</param>
    /// <returns>Instanca kreiranog autora</returns>
    public async Task<Author> CreateAuthor(AuthorCreateDTO authorDto)
    {
        var author = authorDto.MapDtoToDomainEntity();
        await authorRepository.CreateAsync(author);
        return author;
    }

    /// <summary>
    /// Ažurira podatke o autoru
    /// </summary>
    /// <param name="authorId">Jedinstveni identifikator autora koji se ažurira</param>
    /// <param name="updatedAuthor">DTO objekat koji sadrži nove podatke za autora</param>
    /// <returns>DTO objekat tipa <see cref="GetAuthorDTO"/> sa ažuriranim podacima o autoru</returns>
    /// <exception cref="AuthorNotFoundException">Baca se ako autor sa zadatim authorId ne postoji</exception>
    public async Task<GetAuthorDTO> UpdateAuthor(string authorId, AuthorUpdateDTO updatedAuthor)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        await authorRepository.UpdateAsync(updatedAuthor.MapDtoToDomainEntity(author), authorId);
        return author.MapDomainEntityToDto();
    }
}