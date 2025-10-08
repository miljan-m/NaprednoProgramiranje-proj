using System.ComponentModel.DataAnnotations;
using LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;
/// <summary>
/// Klasa <see cref="Customer"/> predstavlja korisnika biblioteke.
/// </summary>
/// <remarks>
/// Klasa sadrži osnovne podatke o korisniku, uključujući ime, prezime i jedinstveni identifikator JMBG.
/// </remarks>

public class Customer : IBaseEntity
{
    /// <summary>
    /// Ime korisnika.
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Prezime korisnika.
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Jedinstveni matični broj građana (JMBG) koji služi kao primarni ključ korisnika.
    /// </summary>
    [Key]
    public string jmbg;
    /// <summary>
    /// Podrazumevani konstruktor klase <see cref="Customer"/>.
    /// </summary>

    public Customer()
    {

    }
    /// <summary>
    /// Parametarski konstruktor klase <see cref="Customer"/>
    /// </summary>
    /// <param name="firstName">Ime korisnika</param>
    /// <param name="lastName">Prezime korisnika</param>
    /// <param name="jmbg">Jedinstveni matični broj korisnika</param>
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="firstName"/>, <paramref name="lastName"/> ili <paramref name="jmbg"/> null ili prazni,
    /// ili ako <paramref name="jmbg"/> ne sadrži tačno 13 cifara.
    /// </exception>
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

    /// <summary>
    /// Poredi dva korisnika na osnovu njihovog JMBG broja.
    /// </summary>
    /// <param name="obj">Objekat sa kojim se vrši poređenje.</param>
    /// <returns>
    /// TRUE ako korisnici imaju isti JMBG; u suprotnom FALSE.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj is Customer other)
        {
            return this.jmbg == other.jmbg;
        }
        return false;
    }
}