using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using LibraryApp.DTOs;
//using LibraryApp.Mappers;
using LibraryApp.Domen.Abstractions;
using LibraryApp.Domen.Interfaces;
//using LibraryApp.Services;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryApp.Domen.Models;


public class Book : IBaseEntity, IBook 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Isbn { get; set; }
    public string Title { get; set; }

    public string Genre { get; set; }
    public bool Available { get; set; }
    public string? AuthorId { get; set; }
    public Author? Author { get; set; }


    public Book()
    {

    }

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

    public virtual string BookDetails()
    {
        return $"ISBN: {Isbn}\n Title: {Title}\n Genre: {Genre}\n Available: {Available} \n Special edition:NO";
    }

    public string ShowAutograph()
    {
        return "This is not special edition book. Autograph IS NOT available";
    }
    public override bool Equals(object obj)
    {
        if (obj is Book other)
        {
            return this.Isbn == other.Isbn;
        }
        return false;
    }
}