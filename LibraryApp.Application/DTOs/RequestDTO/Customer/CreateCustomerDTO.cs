using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Application.DTOs.RequestDTO.Customer;

public class CreateCustomerDTO
{
    public string JMBG { get; set; }
    
    [Length(1, 30, ErrorMessage = "Length of name must be between 1 and 30 characters")]
    public string FirstName { get; set; }
    [Length(1,30, ErrorMessage ="Length of last name must be between 1 and 30 characters")]
    public string LastName { get; set; }
   
}