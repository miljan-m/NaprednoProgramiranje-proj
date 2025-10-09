using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;

namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja poslovnom logikom za entitet <see cref="SpecialEditionBook"/>
/// Omogućava kreiranje, vraćanje, ažuriranje i brisanje specijalnih izdanja knjiga
/// </summary>
public class SpecialEditionBookService : ISpecialEditionBookService
{
    private readonly IJSONService<SpecialEditionBook> bookJSON;

    private readonly IGenericRepository<SpecialEditionBook> specEditionBookRepository;
    private readonly IGenericRepository<Author> authorRepository;
    /// <summary>
    /// Inicijalizuje novi <see cref="SpecialEditionBookService"/> sa prosleđenim repozitorijumima
    /// </summary>
    /// <param name="specEditionBookRepository">Generički repozitorijum za entitet <see cref="SpecialEditionBook"/></param>
    /// <param name="authorRepository">Generički repozitorijum za entitet <see cref="Author"/></param>
    public SpecialEditionBookService(IGenericRepository<SpecialEditionBook> specEditionBookRepository, IGenericRepository<Author> authorRepository, IJSONService<SpecialEditionBook> bookJSON)
    {
        this.specEditionBookRepository = specEditionBookRepository;
        this.authorRepository = authorRepository;
        this.bookJSON = bookJSON;
    }

    /// <summary>
    /// Vraća sve specijalne izdanja knjiga iz baze 
    /// </summary>
    /// <returns>Kolekciju DTO objekata tipa <see cref="GetSpecialBooksDTO"/> koji predstavljaju sve specijalne knjige</returns>
    /// <exception cref="NotFoundException">Baca se ako baza ne sadrži nijednu specijalnu knjigu</exception>
    public async Task<IEnumerable<GetSpecialBooksDTO>> GetBooks()
    {
        var booksList = await specEditionBookRepository.GetAllAsync();
        var books = booksList.Select(b => b.MapDomainEntitiesToDto());
        if (booksList == null) throw new NotFoundException("Database is empty");
        return books;
    }
    /// <summary>
    /// Vraća specijalno izdanje knjige u zavisnosti od ISBN
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige</param>
    /// <returns>DTO objekat tipa <see cref="GetSpecialBookDTO"/> sa podacima o knjizi</returns>
    /// <exception cref="SpecBookInvalidArgumentException">Baca se ako ISBN sadrži nedozvoljene karaktere</exception>
    /// <exception cref="SpecBookNotFoundException">Baca se ako knjiga sa datim ISBN-om ne postoji</exception>
    public async Task<GetSpecialBookDTO> GetBook(string isbn)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+', '*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
        for (int i = 0; i < specChar.Length; i++)
        {
            if (isbn.Contains(specChar[i])) isbnValid = false;
        }
        if (isbnValid == false) throw new SpecBookInvalidArgumentException(isbn);
        var book = await specEditionBookRepository.GetOneAsync(isbn);
        if (book == null) throw new SpecBookNotFoundException(isbn);
        bookJSON.WriteJSONInFile(book);
        return book.MapDomainEntityToDto();
    }
    /// <summary>
    /// Briše specijalno izdanje knjige prema ISBN broju
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige koja se briše</param>
    /// <returns>Vraća TRUE ako je brisanje uspešno.</returns>
    /// <exception cref="SpecBookInvalidArgumentException">Baca se ako ISBN sadrži nedozvoljene karaktere</exception>
    /// <exception cref="SpecBookNotFoundException">Baca se ako knjiga sa datim ISBN-om ne postoji</exception>
    public async Task<bool> DeleteBook(string isbn)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+', '*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
        for (int i = 0; i < specChar.Length; i++)
        {
            if (isbn.Contains(specChar[i])) isbnValid = false;
        }
        if (isbnValid == false) throw new SpecBookInvalidArgumentException(isbn);
        var book = await specEditionBookRepository.GetOneAsync(isbn);
        if (book == null) throw new SpecBookNotFoundException(isbn);
        await specEditionBookRepository.DeleteAsync(isbn);
        return true;
    }
    /// <summary>
    /// Kreira novo specijalno izdanje knjige
    /// </summary>
    /// <param name="bookCreateDTO">DTO objekat tipa <see cref="CreateSpecialBookDTO"/> sa podacima o novoj knjizi</param>
    /// <param name="authorId">Jedinstveni identifikator autora knjige.</param>
    /// <returns>DTO objekat tipa <see cref="GetSpecialBookDTO"/> sa podacima o kreiranoj knjizi</returns>
    /// <exception cref="AuthorNotFoundException">Baca se ako autor sa datim ID-jem ne postoji</exception>
    public async Task<GetSpecialBookDTO> CreateBook(CreateSpecialBookDTO bookCreateDTO, string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        var book = bookCreateDTO.MapDtoToDomainEntity(author);
        await specEditionBookRepository.CreateAsync(bookCreateDTO.MapDtoToDomainEntity(author));
        return book.MapDomainEntityToDto();
    }
    /// <summary>
    /// Ažurira specijalno izdanje knjige.
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige koja se ažurira</param>
    /// <param name="updatedBook">DTO objekat tipa <see cref="UpdateSpecialBookDTO"/> sa novim podacima o knjizi</param>
    /// <returns>DTO objekat tipa <see cref="GetSpecialBookDTO"/> sa ažuriranim podacima o knjizi</returns>
    /// <exception cref="SpecBookInvalidArgumentException">Baca se ako ISBN sadrži nedozvoljene karaktere</exception>
    /// <exception cref="SpecBookNotFoundException">Baca se ako knjiga sa datim ISBN-om ne postoji</exception>
    public async Task<GetSpecialBookDTO> UpdateBook(string isbn, UpdateSpecialBookDTO updatedBook)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+', '*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
        for (int i = 0; i < specChar.Length; i++)
        {
            if (isbn.Contains(specChar[i])) isbnValid = false;
        }
        if (isbnValid == false) throw new SpecBookInvalidArgumentException(isbn);


        //var specialBook = await specEditionBookRepository.Set<SpecialEditionBook>().Include(b => b.Author).Where(b => b.Isbn == isbn).FirstOrDefaultAsync();
        var specialBook = await specEditionBookRepository.GetOneAsync(isbn);

        if (specialBook == null) throw new SpecBookNotFoundException(isbn);

        var book = updatedBook.MapDtoToDomainEntity(specialBook);
        book.Isbn = isbn;
        await specEditionBookRepository.UpdateAsync(book, isbn);
        return specialBook.MapDomainEntityToDto();
    }
}