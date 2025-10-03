namespace LibraryApp.Application.DTOs.ResponseDTO.Admin;

public class GetAdminsDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}