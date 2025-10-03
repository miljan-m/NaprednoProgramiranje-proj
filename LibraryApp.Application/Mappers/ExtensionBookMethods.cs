
namespace LibraryApp.Mappers;

public static class ExtensionBookMethods
{
    public static Book MapDtoToDomainEntity(this BookCreateDTO bookDTO, Author author)
    {

        return new Book
        {
            Isbn = bookDTO.Isbn,
            Title = bookDTO.Title,
            Genre = bookDTO.Genre,
            Available = bookDTO.Available,
            AuthorId = author.AuthorId,
            Author = author
        };
    }

    public static Book MapDtoToDomainEntity(this BookUpdateDTO bookDTO, Book book)
    {
        book.Title = bookDTO.Title;
        book.Genre = bookDTO.Genre;
        book.Available = bookDTO.Available;
        return book;
        
    }

    public static GetBooksDTO MapDomainEntitiesToDTO(this Book book)
    {
        return new GetBooksDTO
        {
            Isbn = book.Isbn,
            Title = book.Title,
            Genre = book.Genre,
            Available = book.Available,
            AuthorName = book.Author != null ? book.Author.Name + " " + book.Author.LastName : "NO AUTHOR"
        };
    }

    public static GetBookDTO MapDomainEntityToDTO(this Book book)
    {
        return new GetBookDTO
        {
            Isbn = book.Isbn,
            Title = book.Title,
            Genre = book.Genre,
            Available = book.Available,
            AuthorName = book.Author!=null ? book.Author.Name+" "+book.Author.LastName : "NO AUTHOR"
        };     
    }

}