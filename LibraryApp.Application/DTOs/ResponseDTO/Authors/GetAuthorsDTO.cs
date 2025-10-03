namespace LibraryApp.Application.DTOs.ResponseDTO.Authors;

public class GetAuthorsDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public List<GetBooksDTO> Books { get; set; }


}