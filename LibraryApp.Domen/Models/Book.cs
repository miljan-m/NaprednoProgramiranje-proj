using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Domen.Abstractions;
using LibraryApp.Domen.Interfaces;

namespace LibraryApp.Domen.Models;

/// <summary>
/// Klasa <see cref="Book"/> predstavlja osnovni entitet knjige u sistemu.
/// </summary>
/// <remarks>
/// Klasa sadrži osnovne informacije o knjizi — ISBN, naslov, žanr, dostupnost i referencu na autora.
/// Implementira interfejse <see cref="IBaseEntity"/> i <see cref="IBook"/>.
/// </remarks>
public class Book : IBaseEntity, IBook
{
    /// <summary>
    /// Jedinstveni identifikator knjige
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Isbn { get; set; }
    /// <summary>
    /// Naslov knjige
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Žanr kojem knjiga pripada
    /// </summary>
    public string Genre { get; set; }
    /// <summary>
    /// Oznaka koja pokazuje da li je knjiga trenutno dostupna
    /// </summary>
    public bool Available { get; set; }
    /// <summary>
    /// Identifikator autora koji je napisao knjigu.
    /// </summary>
    public string? AuthorId { get; set; }
    /// <summary>
    /// Referenca na objekat klase Author koji predstavlja autora knjige.
    /// </summary>
    public Author? Author { get; set; }

    /// <summary>
    /// Podrazumevani konstruktor klase Book
    /// </summary>
    public Book()
    {

    }
    /// <summary>
    /// Parametarski konstruktor klase <see cref="Book"/> koji omogućava kreiranje instance sa zadatim vrednostima.
    /// </summary>
    /// <param name="isbn">Jedinstveni ISBN identifikator knjige.</param>
    /// <param name="title">Naslov knjige.</param>
    /// <param name="genre">Žanr knjige.</param>
    /// <param name="available">Pokazatelj da li je knjiga dostupna.</param>
    /// <param name="authorId">Opcioni identifikator autora.</param>
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="isbn"/> ili <paramref name="title"/> null ili prazni stringovi.
    /// </exception>
    public Book(string isbn, string title, string genre, bool available, string? authorId = null)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be null or empty.", nameof(isbn));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));

        Isbn = isbn;
        Title = title;
        Genre = genre;
        Available = available;
        AuthorId = authorId;
    }
    /// <summary>
    /// Prikazuje detalje o knjizi
    /// </summary>
    /// <returns>
    /// Tekstualni opis knjige 
    /// </returns>
    public virtual string BookDetails()
    {
        return $"ISBN: {Isbn}\n Title: {Title}\n Genre: {Genre}\n Available: {Available} \n Special edition:NO";
    }
    /// <summary>
    /// Prikazuje autogram autora knjige
    /// </summary>
    /// <returns>
    /// Poruku koja označava da knjiga nije specijalno izdanje i da autogram nije dostupan(vazi samo za obicnu knjigu)
    /// </returns>
    public string ShowAutograph()
    {
        return "This is not special edition book. Autograph IS NOT available";
    }
    /// <summary>
    /// Upoređuje dve knjige na osnovu njihovog ISBN broja.
    /// </summary>
    /// <param name="obj">Objekat sa kojim se vrši poređenje.</param>
    /// <returns>
    /// <c>true</c> ako knjige imaju isti ISBN; u suprotnom <c>false</c>.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj is Book other)
        {
            return this.Isbn == other.Isbn;
        }
        return false;
    }
}