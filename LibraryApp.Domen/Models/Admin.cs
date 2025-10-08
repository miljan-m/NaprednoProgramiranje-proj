using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using  LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;

public class Admin : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string AdminId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public Admin()
    {

    }
    public Admin(string adminId, string firstName, string lastName, DateTime? dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(adminId))
            throw new ArgumentException("Admin ID cannot be null or empty.", nameof(adminId));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

        AdminId = adminId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
        
    public override bool Equals(object obj)
    {
        if (obj is Admin other)
        {
            return this.AdminId == other.AdminId;
        }
        return false;
    }
}