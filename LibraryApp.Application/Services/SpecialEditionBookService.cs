using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;

namespace LibraryApp.Application.Services;

public class SpecialEditionBookService : ISpecialEditionBookService
{
    
    private readonly IGenericRepository<SpecialEditionBook> specEditionBookRepository;
    private readonly IGenericRepository<Author> authorRepository;

    public SpecialEditionBookService(IGenericRepository<SpecialEditionBook> specEditionBookRepository, IGenericRepository<Author> authorRepository)
    {
        this.specEditionBookRepository = specEditionBookRepository;
        this.authorRepository = authorRepository;
    }


    public async Task<IEnumerable<GetSpecialBooksDTO>> GetBooks()
    {
        var booksList = await specEditionBookRepository.GetAllAsync();
        var books = booksList.Select(b => b.MapDomainEntitiesToDto());
        if (booksList == null) throw new NotFoundException("Database is empty");
        return books;
    }

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
        return book.MapDomainEntityToDto();
    }

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
    public async Task<GetSpecialBookDTO> CreateBook(CreateSpecialBookDTO bookCreateDTO, string authorId)
    {
        var author = await authorRepository.GetOneAsync(authorId);
        if (author == null) throw new AuthorNotFoundException(authorId);
        var book = bookCreateDTO.MapDtoToDomainEntity(author);
        await specEditionBookRepository.CreateAsync(bookCreateDTO.MapDtoToDomainEntity(author));
        return book.MapDomainEntityToDto();
    }

    public async Task<GetSpecialBookDTO> UpdateBook(string isbn, UpdateSpecialBookDTO updatedBook)
    {
        bool isbnValid = true;
        char[] specChar = ['*', '\'', '\\', '+','*', '/', '.', ',', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '|', '[', ']'];
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