using FluentValidation;

namespace LibraryApp.Application.Validators;

public class AdminValidator : AbstractValidator<Admin>
{
    public AdminValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name cannot be empty string");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Name cannot be empty string");
        RuleFor(x => x.AdminId).NotEqual("0").WithMessage("Id cannot be 0");
        
    }
}