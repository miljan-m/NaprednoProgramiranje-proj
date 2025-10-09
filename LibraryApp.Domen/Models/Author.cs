using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;
/// <summary>
/// Predstavlja autora jedne ili više knjiga u sistemu biblioteke.
/// </summary>
/// <remarks>
/// Klasa <see cref="Author"/> čuva osnovne informacije o autoru,
/// uključujući ime, prezime i opcioni datum rođenja.  
/// </remarks>

public class Author : IBaseEntity
{   /// <summary>
    /// Jedinstveni identifikator autora.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string AuthorId { get; set; }
    /// <summary>
    /// Ime autora.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Prezime autora.
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Datum rođenja autora.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// Lista knjiga koje je napisao autor.
    /// </summary>
    public List<Book> Books { get; set; }
    /// <summary>
    /// Podrazumevani konstruktor klase <see cref="Author"/>.
    /// </summary>
    public Author()
    {

    }
    /// <summary>
    /// Kreira novu instancu klase <see cref="Author"/> sa zadatim vrednostima.
    /// </summary>
    /// <param name="name">Ime autora. Ne sme biti null ili prazno.</param>
    /// <param name="lastName">Prezime autora. Ne sme biti null ili prazno.</param>
    /// <param name="dateOfBirth">Datum rođenja autora. Može biti null, ali ne sme biti u budućnosti.</param>
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="name"/> ili <paramref name="lastName"/> prazni,
    /// ili ako je <paramref name="dateOfBirth"/> u budućnosti.
    /// </exception>
    public Author(string name, string lastName, DateTime? dateOfBirth = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Author name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Author last name cannot be null or empty.", nameof(lastName));

        if (dateOfBirth.HasValue && dateOfBirth.Value > DateTime.Now)
            throw new ArgumentException("Date of birth cannot be in the future.", nameof(dateOfBirth));

        Name = name;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }


    /// <summary>
    /// Poredi trenutnog autora sa drugim objektom.
    /// </summary>
    /// <param name="obj">Objekat sa kojim se vrši poređenje.</param>
    /// <returns>
    /// Vraća TRUE ako je prosleđeni objekat tipa Author
    /// i ima isti AuthorId; u suprotnom vraća FALSE.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj is Author other)
        {
            return this.AuthorId == other.AuthorId;
        }
        return false;
    }
}