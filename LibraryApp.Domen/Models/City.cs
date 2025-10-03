using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Domen.Models;

public class City
{
    [Key]
    public string PostalCode { get; set; }
    
    public string CityName { get; set; }
    
    
}