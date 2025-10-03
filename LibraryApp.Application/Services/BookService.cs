using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;

namespace LibraryApp.Application.Services;

public class BookService : IBookService
{
    
    private readonly IGenericRepository<Book> bookRepository;
    private readonly IGenericRepository<Author> authorRepository;
    public BookService(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository)
    {
        
        this.bookRepository = bookRepository;
        this.authorRepository = authorRepository;
    }

    public async Task<IEnumerable<GetBooksDTO>> GetBooks()
    {
        var booksList = await bookRepository.GetAllAsync();
        var books = booksList.Select(b => b.MapDomainEntitiesToDTO());
        if (booksList == null) throw new NotFoundException("Database is empty");
        return books;
    }

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
        return book.MapDomainEntityToDTO();
    }

    public async Task<GetBookDTO> CreateBook(BookCreateDTO bookCreateDTO, string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        var book = bookCreateDTO.MapDtoToDomainEntity(author);

        await bookRepository.CreateAsync(book);

        return book.MapDomainEntityToDTO();
    }

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