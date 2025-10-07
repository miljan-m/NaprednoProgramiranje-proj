using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace LibraryApp.Domen.Models;

public class Address
{
    [Key]
    public string? id { get; set; }
    public int? number { get; set; }
    public string street { get; set; }
    public City City { get; set; }
    public string PostalCode { get; set; }

    public Address()
    {

    }
    public Address(string id, int number, string street, City City, string PostalCode)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id cannot be null or empty.", nameof(id));

        if (number <= 0)
            throw new ArgumentException("Number must be greater than zero.", nameof(number));

        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be null or empty.", nameof(street));

        this.City = City ?? throw new ArgumentNullException(nameof(City), "City cannot be null.");

        if (string.IsNullOrWhiteSpace(PostalCode))
            throw new ArgumentException("Postal code cannot be null or empty.", nameof(PostalCode));

        this.id = id;
        this.number = number;
        this.street = street;
        this.PostalCode = PostalCode;
    }

    public override bool Equals(object obj)
        {
            if (obj is Address other)
            {
                return this.PostalCode == other.PostalCode &&
                       this.street == other.street &&
                       this.number == other.number;
            }
            return false;
        }

}