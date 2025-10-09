using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Domen.Models;

/// <summary>
/// Klasa <see cref="City"/> predstavlja grad u kojem se nalazi određena adresa.
/// </summary>
/// <remarks>
/// Klasa sadrži osnovne informacije o gradu — poštanski broj i naziv grada.
/// Koristi se kao referenca u klasi <see cref="Address"/> kako bi se uspostavila veza između adrese i grada kojem pripada.
/// </remarks>
public class City
{


    /// <summary>
    /// PostalCode predstavlja jedinstveni identifikator grada, odnosno njegov poštanski broj.
    /// </summary>
    [Key]
    public string PostalCode { get; set; }
    /// <summary>
    /// CityName predstavlja naziv grada.
    /// </summary>
    public string CityName { get; set; }
    /// <summary>
    /// Podrazumevani konstruktor klase <see cref="City"/>.
    /// </summary>
    public City()
    {

    }
    /// <summary>
    /// Parametarski konstruktor klase <see cref="City"/> koji omogućava kreiranje instance sa zadatim vrednostima.
    /// </summary>
    /// <param name="postalCode">Poštanski broj grada </param>
    /// <param name="cityName">Naziv grada</param>
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="postalCode"/> ili <paramref name="cityName"/> null ili prazni stringovi.
    /// </exception>
    public City(string postalCode, string cityName)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code cannot be null or empty.", nameof(postalCode));

        if (string.IsNullOrWhiteSpace(cityName))
            throw new ArgumentException("City name cannot be null or empty.", nameof(cityName));

        PostalCode = postalCode;
        CityName = cityName;
    }
    /// <summary>
    /// Equals metoda je override tako da upoređuje dva grada na osnovu njihovog poštanskog broja.
    /// </summary>
    /// <param name="obj">Parametar tipa City koji se poredi sa trenutnim objektom.</param>
    /// <returns>
    /// TRUE ako su gradovi jednaki po poštanskom broju; 
    /// u suprotnom vraća FALSE.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj is City other)
        {
            return this.PostalCode == other.PostalCode;
        }
        return false;
    }
}