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

    public Author(string name, string lastName, DateTime? dateOfBirth = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Author name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Author last name cannot be null or empty.", nameof(lastName));

        if (dateOfBirth.HasValue && dateOfBirth.Value > DateTime.Now)
            throw new ArgumentException("Date of birth cannot be in the future.", nameof(dateOfBirth));

        Name = name;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }


    public override bool Equals(object obj)
    {
        if (obj is Author other)
        {
            return this.AuthorId == other.AuthorId;
        }
        return false;
    }
}