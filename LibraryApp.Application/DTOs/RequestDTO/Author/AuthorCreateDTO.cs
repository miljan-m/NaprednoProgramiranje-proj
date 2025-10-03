using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Application.DTOs.RequestDTO.Author;

public class AuthorCreateDTO
{   
    [Length(1,30, ErrorMessage ="Length of name must be between 1 and 30 characters")]
    public string Name { get; set; }
    [Length(1,30, ErrorMessage ="Length of last name must be between 1 and 30 characters")]
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
 
}