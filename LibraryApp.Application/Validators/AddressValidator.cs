using FluentValidation;
using LibraryApp.Application.DTOs.RequestDTO.Address;

namespace LibraryApp.Application.Validators;
public class AddressValidator : AbstractValidator<AddressCreateDTO>
{
    public AddressValidator()
    {
        RuleFor(x => x.number).GreaterThanOrEqualTo(0).WithMessage("Number must be entered");
        RuleFor(x => x.street).NotEmpty().WithMessage("Street name cannot be empty string");
    }
}