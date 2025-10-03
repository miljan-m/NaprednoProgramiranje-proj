using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;


public class Author : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string AuthorId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public List<Book> Books { get; set; }
    
    public Author()
    {

    }

     public Author(string AuthorId, string Name, string LastName)
    {
        this.AuthorId = AuthorId;
        this.Name = Name;
        this.LastName = LastName;
    }
}