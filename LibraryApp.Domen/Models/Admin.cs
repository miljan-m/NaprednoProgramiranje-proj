using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Domen.Abstractions;

namespace LibraryApp.Domen.Models;
/// <summary>
/// Klasa <see cref="Admin"/> predstavlja administratora sistema biblioteke.
/// </summary>
/// <remarks>
/// Administrator ima mogućnost upravljanja korisnicima, knjigama i sistemskim podešavanjima.
/// Klasa sadrži osnovne informacije o administratoru — jedinstveni identifikator, ime, prezime i datum rođenja.
/// </remarks>
public class Admin : IBaseEntity
{
    /// <summary>
    /// Jedinstveni identifikator administratora u sistemu.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string AdminId { get; set; }
    /// <summary>
    /// Ime administratora.
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Prezime administratora.
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Datum rođenja administratora. Može biti null ako podatak nije poznat.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// Podrazumevani konstruktor klase <see cref="Admin"/>.
    /// </summary>
    public Admin()
    {

    }
    /// <summary>
    /// Parametarski konstruktor klase <see cref="Admin"/> koji omogućava kreiranje instance sa zadatim vrednostima.
    /// </summary>
    /// <param name="adminId">Jedinstveni identifikator administratora</param>
    /// <param name="firstName">Ime administratora</param>
    /// <param name="lastName">Prezime administratora </param>
    /// <param name="dateOfBirth">Datum rođenja administratora</param>
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="adminId"/>, <paramref name="firstName"/> ili <paramref name="lastName"/> null ili prazni stringovi.
    /// </exception>
    public Admin(string adminId, string firstName, string lastName, DateTime? dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(adminId))
            throw new ArgumentException("Admin ID cannot be null or empty.", nameof(adminId));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

        AdminId = adminId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
    /// <summary>
    /// Equals metoda je override i omogućava poređenje dva administratora
    /// na osnovu njihovog jedinstvenog identifikatora .
    /// </summary>
    /// <param name="obj">Objekat tipa <see cref="Admin"/> koji se poredi sa trenutnim instancom.</param>
    /// <returns>
    /// TRUE ako su administratori jednaki po <see cref="AdminId"/>;  
    /// u suprotnom vraća FALSE.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj is Admin other)
        {
            return this.AdminId == other.AdminId;
        }
        return false;
    }
}