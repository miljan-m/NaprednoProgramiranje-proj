using LibraryApp.Domen.Interfaces;

namespace LibraryApp.Domen.Models;

/// <summary>
/// Klasa <see cref="SpecialEditionBook"/> predstavlja specijalno izdanje knjige
/// </summary>
public class SpecialEditionBook : Book, IBook
{   /// <summary>
    /// Broj dostupnih primeraka specijalnog izdanja u skladištu.
    /// </summary>
    public int InStorage { get; set; }
    /// <summary>
    /// Autogram autora 
    /// </summary>
    private string autograph;
    public string Autograph { get => autograph; set => autograph = value; }
    /// <summary>
    /// Podrazumevani konstruktor klase <see cref="SpecialEditionBook"/>.
    /// </summary>
    public SpecialEditionBook()
    {

    }
    /// <summary>
    /// Parametarski konstruktor klase <see cref="SpecialEditionBook"/> koji omogućava kreiranje
    /// specijalnog izdanja sa zadatim vrednostima.
    /// </summary>
    /// <param name="isbn">Jedinstveni identifikator knjige</param>
    /// <param name="title">Naslov knjige</param>
    /// <param name="genre">Žanr kojem knjiga pripada</param>
    /// <param name="available">Parametar koji prikazuje da li je knjiga dostupna za izdavanje</param>
    /// <param name="autograph">Autogram autora knjige</param>
    /// <param name="inStorage">Broj dostupnih primeraka knjige</param>
    /// <param name="authorId">Id autora</param>
    /// <exception cref="ArgumentException">
    /// Baca se ako su <paramref name="autograph"/> null ili prazan string,
    /// ili ako je <paramref name="inStorage"/> negativan broj.
    /// </exception>
    public SpecialEditionBook(string isbn, string title, string genre, bool available, string autograph, int inStorage = 0, string? authorId = null)
            : base(isbn, title, genre, available, authorId)
    {
        if (string.IsNullOrWhiteSpace(autograph))
            throw new ArgumentException("Autograph cannot be null or empty.", nameof(autograph));

        if (inStorage < 0)
            throw new ArgumentException("InStorage cannot be negative.", nameof(inStorage));

        this.autograph = autograph;
        InStorage = inStorage;
    }
    /// <summary>
    /// Prikazuje detalje o specijalnom izdanju knjige
    /// </summary>
    /// <returns>
    /// String koji sadrži sve osnovne informacije o specijalnom izdanju.
    /// </returns>
    public override string BookDetails()
    {
        return $"ISBN: {Isbn}\n Title: {Title}\n Genre: {Genre}\n Available: {Available}\n Special edition:YES\n Autograph: {Autograph}";
    }
    /// <summary>
    /// Vraća autogram autora.
    /// </summary>
    /// <returns>
    /// String koji predstavlja autograma autora
    /// </returns>
    new public string ShowAutograph()
    {
        return Autograph;
    }

    /// <summary>
    /// Poredi dve knjige na osnovu njihovog ISBN broja.
    /// </summary>
    /// <param name="obj">Objekat sa kojim se vrši poređenje.</param>
    /// <returns>
    /// TRUE ako knjige imaju isti ISBN; u suprotnom FALSE
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj is SpecialEditionBook other)
        {
            return this.Isbn == other.Isbn;
        }
        return false;
    }
}