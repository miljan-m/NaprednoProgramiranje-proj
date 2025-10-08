using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace LibraryApp.Domen.Models;

/// <summary>
/// Ova klasa predstavlja adresu koja je povezana sa odredjenim gradom
/// </summary>
/// <remarks>
/// Ova klasa sadrzi osnovne informacije o adresi. Rec je o broju ulice, nazivu ulice, gradu i njegovom postanskom broju
/// <remarks/>

public class Address
{
   

    /// <summary>
    /// Id je jedinstveni identifikator za klasu address
    /// </summary>
    [Key]
    public string? id { get; set; }
    /// <summary>
    /// Number predstavlja broj ulice za datu adresu
    /// </summary>
    public int? number { get; set; }
    /// <summary>
    /// Street je atribut koji predstavlja naziv ulice
    /// </summary>
    public string street { get; set; }
    /// <summary>
    /// City je atribut na koji adresa ima referencu i odnosi se na grad u kojem se nalazi ta adres
    /// </summary>
    public City City { get; set; }
    /// <summary>
    /// PostalCode predstavlja postanski broj grada
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Podrazumevani konstruktor klase <see cref="Address"/>.
    /// </summary>
    public Address()
    {

    }

    /// <summary>
    /// Parametarski konstruktor klase <see cref="Address"/> sa zadatim vrednostima.
    /// </summary>
    /// <param name="id">Jedinstveni identifikator adrese</param>
    /// <param name="number">Broj objekta u okviru ulice </param>
    /// <param name="street">Naziv ulice</param>
    /// <param name="City">Grad kojem adresa pripada</param>
    /// <param name="PostalCode">Poštanski broj</param>
    
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="id"/>, <paramref name="street"/> ili <paramref name="PostalCode"/> null ili prazni,
    /// ili ako je <paramref name="number"/> manji ili jednak nuli/> null.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Baca se ako je <paramref name="City"/> null.
    /// </exception>
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
    /// <summary>
    /// Equals metoda je override tako da uporedjuje dve adrese na osnovu postanskog broja, tj. grada, zatim adrese i broja
    /// </summary>
    /// <param name="obj">Parametar tipa Address</param>
    /// <returns>Vraca TRUE ako su adrese jednake po parametrima koji su malopre pomenuti ili FALSE ako nisu</returns>
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