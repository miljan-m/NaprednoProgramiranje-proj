using FluentValidation;

namespace LibraryApp.Application.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty string");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty string");
        RuleFor(x => x.DateOfBirth).NotNull().WithMessage("Date of birth must be entered");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth must be entered");

        
    }
}