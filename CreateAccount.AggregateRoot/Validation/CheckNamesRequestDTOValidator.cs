using CreateAccount.DTO.DTOs;
using FluentValidation;

public class CheckNamesRequestDTOValidator : AbstractValidator<CheckNamesRequestDTO>
{
    public CheckNamesRequestDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required.")
            .Length(3, 20).WithMessage("UserName must be between 3 and 20 characters.");

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("CompanyName is required.");
    }
}
