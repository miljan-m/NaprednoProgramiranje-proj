using System.ComponentModel.DataAnnotations;
using LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;


public class Customer : IBaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string jmbg;

    [Key]
    public string JMBG
    {
        get => jmbg;
        set
        {
            if (long.Parse(value) < 0) throw new Exception("JMBG cannot be less than 0");
           // if (value.Length != 13) throw new Exception("JMBG must have length of 13");
            jmbg = value;
        }
    }

    public Customer()
    {

    }
    public Customer(string FirstName, string LastName, string JMBG)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.JMBG = JMBG;
    }
}