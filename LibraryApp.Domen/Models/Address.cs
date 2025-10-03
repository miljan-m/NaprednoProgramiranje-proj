using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace LibraryApp.Domen.Models;

public class Address
{
    [Key]
    public string id { get; set; }
    public int? number { get; set; }
    public string street { get; set; }
    public City City { get; set; }
    public string PostalCode { get; set; }

public Address()
    {

    }

}