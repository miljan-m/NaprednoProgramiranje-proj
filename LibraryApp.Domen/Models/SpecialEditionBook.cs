using LibraryApp.Domen.Abstractions;
using LibraryApp.Domen.Interfaces;

namespace LibraryApp.Domen.Models;


public class SpecialEditionBook : Book, IBook
{
    public int InStorage { get; set; }
    private string autograph;
    public string Autograph { get => autograph; set => autograph = value; }
    public SpecialEditionBook()
    {

    }
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

    public override string BookDetails()
    {
        return $"ISBN: {Isbn}\n Title: {Title}\n Genre: {Genre}\n Available: {Available}\n Special edition:YES\n Autograph: {Autograph}";
    }

    new public string ShowAutograph()
    {
        return Autograph;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is SpecialEditionBook other)
        {
            return this.Isbn == other.Isbn;
        }
        return false;
    }
}