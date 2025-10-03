using FluentValidation;

namespace LibraryApp.Application.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name must be entered");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty string");
        RuleFor(x => x.jmbg).Length(13).WithMessage("JMBG must be 13 diggit number");
    }
}