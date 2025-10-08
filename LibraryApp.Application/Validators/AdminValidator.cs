using FluentValidation;

namespace LibraryApp.Application.Validators;

public class AdminValidator : AbstractValidator<Admin>
{
    public AdminValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name cannot be empty string");
        RuleFor(x => x.FirstName).NotNull().WithMessage("Name cannot be null");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Name cannot be empty string");
        RuleFor(x => x.LastName).NotNull().WithMessage("LastName cannot be null");

        RuleFor(x => x.AdminId).NotEqual("0").WithMessage("Id cannot be 0");
        RuleFor(x => x.AdminId).NotNull().WithMessage("Id cannot be null");
        RuleFor(x => x.AdminId).NotEmpty().WithMessage("Id cannot be 0");

        
    }
}