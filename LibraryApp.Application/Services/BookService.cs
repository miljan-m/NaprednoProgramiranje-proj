using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;

namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja poslovnom logikom za entitet <see cref="Book"/>.
/// Omogućava kreiranje, preuzimanje, ažuriranje i brisanje knjiga
/// </summary>
public class BookService : IBookService
{
    
    private readonly IGenericRepository<Book> bookRepository;
    private readonly IGenericRepository<Author> authorRepository;
    private readonly IJSONService<Book> bookJSON;

    /// <summary>
    /// Inicijalizuje novi <see cref="BookService"/> sa prosleđenim repozitorijumima za knjige i autore
    /// </summary>
    /// <param name="bookRepository">Generički repozitorijum za entitet <see cref="Book"/></param>
    /// <param name="authorRepository">Generički repozitorijum za entitet <see cref="Author"/></param>
    public BookService(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository, IJSONService<Book> bookJSON)
    {

        this.bookRepository = bookRepository;
        this.authorRepository = authorRepository;
        this.bookJSON = bookJSON;
    }
/// <summary>
    /// Vraca sve knjige iz baze podataka.
    /// </summary>
    /// <returns>Kolekciju DTO objekata tipa <see cref="GetBooksDTO"/> koja predstavlja sve knjige</returns>
    /// <exception cref="NotFoundException">Baca se ako baza ne sadrži nijednu knjigu</exception>
    public async Task<IEnumerable<GetBooksDTO>> GetBooks()
    {
        var booksList = await bookRepository.GetAllAsync();
        var books = booksList.Select(b => b.MapDomainEntitiesToDTO());
        if (booksList == null) throw new NotFoundException("Database is empty");
        return books;
    }
    /// <summary>
    /// Vraca knjigu prema sa datim ISBN 
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige</param>
    /// <returns>DTO objekat tipa <see cref="GetBookDTO"/> sa podacima o knjizi</returns>
    /// <exception cref="BookInvalidArgumentException">Baca se ako ISBN sadrži nedozvoljene karaktere</exception>
    /// <exception cref="BookNotFoundException">Baca se ako knjiga sa datim ISBN-om ne postoji</exception>
    public async Task<GetBookDTO> GetBook(string isbn)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+', '*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
        for (int i = 0; i < specChar.Length; i++)
        {
            if (isbn.Contains(specChar[i])) isbnValid = false;
        }
        if (isbnValid == false) throw new BookInvalidArgumentException(isbn);
        var book = await bookRepository.GetOneAsync(isbn);
        if (book == null) throw new BookNotFoundException(isbn);
        bookJSON.WriteJSONInFile(book);
        return book.MapDomainEntityToDTO();
    }
    /// <summary>
    /// Kreira novu knjigu i povezuje je sa autorom
    /// </summary>
    /// <param name="bookCreateDTO">DTO objekat koji sadrži podatke o novoj knjizi</param>
    /// <param name="authorId">Jedinstveni identifikator autora knjige</param>
    /// <returns>DTO objekat tipa <see cref="GetBookDTO"/> sa podacima o novoj knjizi.</returns>
    /// <exception cref="AuthorNotFoundException">Baca se ako autor sa datim ID-jem ne postoji</exception>
    public async Task<GetBookDTO> CreateBook(BookCreateDTO bookCreateDTO, string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        var book = bookCreateDTO.MapDtoToDomainEntity(author);

        await bookRepository.CreateAsync(book);

        return book.MapDomainEntityToDTO();
    }
    /// <summary>
    /// Briše knjigu sa zadatim ISBN
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige</param>
    /// <returns>Vraća TRUE ako je brisanje uspešno.</returns>
    /// <exception cref="BookInvalidArgumentException">Baca se ako ISBN sadrži nedozvoljene karaktere.</exception>
    /// <exception cref="BookNotFoundException">Baca se ako knjiga sa datim ISBN-om ne postoji.</exception>
    public async Task<bool> DeleteBook(string isbn)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+', '*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
        for (int i = 0; i < specChar.Length; i++)
        {
            if (isbn.Contains(specChar[i])) isbnValid = false;
        }
        if (isbnValid == false) throw new BookInvalidArgumentException(isbn);
        var book = await bookRepository.GetOneAsync(isbn);
        if (book == null)  throw new BookNotFoundException(isbn);
        await bookRepository.DeleteAsync(isbn);
        return true; 

    }
     /// <summary>
    /// Ažurira postojeću knjigu na osnovu ISBN 
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige koja se ažurira</param>
    /// <param name="updatedBook">DTO objekat sa novim podacima za knjigu</param>
    /// <returns>DTO objekat tipa <see cref="GetBookDTO"/> sa ažuriranim podacima o knjizi</returns>
    /// <exception cref="BookInvalidArgumentException">Baca se ako ISBN ima nedozvoljene karaktere</exception>
    /// <exception cref="BookNotFoundException">Baca se ako knjiga sa datim ISBN-om ne postoji u bazi</exception>
    public  async Task<GetBookDTO> UpdateBook(string isbn, BookUpdateDTO updatedBook)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+', '*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
        for (int i = 0; i < specChar.Length; i++)
        {
            if (isbn.Contains(specChar[i])) isbnValid = false;
        }
        if (isbnValid == false)  throw new BookInvalidArgumentException(isbn);

        // var book =  context.Books
        //             .OfType<Book>()
        //             .Include(a => a.Author)
        //             .FirstOrDefault(b => b.Isbn == isbn);

        var book =await bookRepository.GetOneAsync(isbn);

        
        if (book == null) throw new BookNotFoundException(isbn);
        var updatedBookToEntity = updatedBook.MapDtoToDomainEntity(book);
        updatedBookToEntity.Isbn = isbn;
        await bookRepository.UpdateAsync(updatedBookToEntity, isbn);
        return book.MapDomainEntityToDTO();
    }
}