using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Domen.Models;

public class City
{
    [Key]
    public string PostalCode { get; set; }

    public string CityName { get; set; }
    public City()
    {
        
    }
    public City(string postalCode, string cityName)
    {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code cannot be null or empty.", nameof(postalCode));

            if (string.IsNullOrWhiteSpace(cityName))
                throw new ArgumentException("City name cannot be null or empty.", nameof(cityName));

            PostalCode = postalCode;
            CityName = cityName;
    }
    public override bool Equals(object obj)
    {
        if (obj is City other)
        {
            return this.PostalCode == other.PostalCode;
        }
        return false;
    }
}