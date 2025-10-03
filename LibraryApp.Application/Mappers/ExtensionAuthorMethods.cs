
namespace LibraryApp.Application.Mappers;

public static class ExtensionAuthorMethods
{
    public static GetAuthorsDTO MapDomainEntitiesToDto(this Author a)
    {
        return new GetAuthorsDTO
        {
            Name = a.Name,
            LastName = a.LastName,
            DateOfBirth = a.DateOfBirth,
        };
    }

    public static GetAuthorDTO MapDomainEntityToDto(this Author a)
    {
        return new GetAuthorDTO
        {
            Name = a.Name,
            LastName = a.LastName,
            DateOfBirth = a.DateOfBirth,
        };
    }

    public static Author MapDtoToDomainEntity(this AuthorCreateDTO authorDto)
    {
        return new Author
        {
            Name = authorDto.Name,
            LastName = authorDto.LastName,
            DateOfBirth = authorDto.DateOfBirth
        };

    }

    public static Author MapDtoToDomainEntity(this AuthorUpdateDTO authorDto, Author author)
    {
        author.Name = authorDto.Name;
        author.LastName = authorDto.LastName;
        author.DateOfBirth = authorDto.DateOfBirth;
        return author;

    }



}