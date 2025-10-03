using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Application.DTOs.RequestDTO.Book;


public class BookCreateDTO
{
    [Key]
    public string Isbn { get; set; }
    [Length(0,25, ErrorMessage ="Length of book's title must be between 1 and 25 characters")]
    public string Title { get; set; }
    
    [AllowedValues("Adventure","Historical","Sci-fi","Action","Crime","Romance")]
    public string Genre { get; set; }
    public bool Available { get; set; }
    
}