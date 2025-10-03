using FluentValidation;

namespace LibraryApp.Application.Validators;

public class CityValidator : AbstractValidator<City>
{
    public CityValidator()
    {
        RuleFor(x => x.CityName).NotEmpty().WithMessage("City name cannot be empty string");
        RuleFor(x => x.PostalCode).NotNull().WithMessage("Postal code cannot be null");
        RuleFor(x => x.PostalCode)
        .NotEmpty().WithMessage("Postal code is required")
        .Matches(@"^\d+$").WithMessage("Postal code must contain only numbers")
        .Must(x => int.Parse(x) > 0).WithMessage("Postal code must be greater than 0");

    }
}