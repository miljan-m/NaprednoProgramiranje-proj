using FluentValidation;

namespace LibraryApp.Application.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty string");
        RuleFor(x => x.Name).NotNull().WithMessage("Name cannot be null");
        RuleFor(x => x.LastName).NotNull().WithMessage("LastName cannot be null");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty string");

        
    }
}