using LibraryApp.Domen.Abstractions;
using LibraryApp.Domen.Interfaces;

namespace LibraryApp.Domen.Models;


public class SpecialEditionBook : Book, IBook 
{
    public int InStorage { get; set; }
    private string autograph;
    public string Autograph { get => autograph; set => autograph = value; }

    public override string BookDetails()
    {
        return $"ISBN: {Isbn}\n Title: {Title}\n Genre: {Genre}\n Available: {Available}\n Special edition:YES\n Autograph: {Autograph}";
    }

    new public string ShowAutograph()
    {
        return Autograph;
    }
}