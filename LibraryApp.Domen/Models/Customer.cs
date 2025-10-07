using System.ComponentModel.DataAnnotations;
using LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;


public class Customer : IBaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Key]
    public string jmbg;


    public Customer()
    {

    }
    public Customer(string firstName, string lastName, string jmbg)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(jmbg))
            throw new ArgumentException("JMBG cannot be null or empty.", nameof(jmbg));

        if (jmbg.Length != 13 || !long.TryParse(jmbg, out _))
            throw new ArgumentException("JMBG must contain exactly 13 digits.", nameof(jmbg));

        FirstName = firstName;
        LastName = lastName;
        this.jmbg = jmbg;
    }
        
    public override bool Equals(object obj)
    {
        if (obj is Customer other)
        {
            // Jednako ako je JMBG isti
            return this.jmbg == other.jmbg;
        }
        return false;
    }
}