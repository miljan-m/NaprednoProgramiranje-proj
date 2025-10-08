namespace LibraryApp.Application.DTOs.ResponseDTO.Authors;

public class GetAuthorDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public List<GetBooksDTO> Books{ get; set; }

    public GetAuthorDTO()
    {
        
    }
    public GetAuthorDTO(string name, string lastname, DateTime date, List<GetBooksDTO> Books)
    {
        Name = name;
        LastName = lastname;
        DateOfBirth = date;
        this.Books = Books;
    }
}