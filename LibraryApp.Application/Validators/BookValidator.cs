using FluentValidation;

namespace LibraryApp.Application.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author id must be entered");
        RuleFor(x => x.Isbn).NotEmpty().WithMessage("ISBN cannot be empty string");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty string");
        RuleFor(x => x.Genre).NotEmpty().WithMessage("Genre cannot be empty string");
    }
}