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
    public Admin(string AdminId, string FirstName, string LastName)
    {
        this.AdminId = AdminId;
        this.FirstName = FirstName;
        this.LastName = LastName;
    }
}