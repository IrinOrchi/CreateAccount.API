using CreateAccount.DTO.DTOs;
using FluentValidation;

public class IndustryTypeRequestDTOValidator : AbstractValidator<IndustryTypeRequestDTO>
{
    public IndustryTypeRequestDTOValidator()
    {
        // Validation for IndustryId, allowing -1 as a valid value or greater
        RuleFor(x => x.IndustryId)
            .GreaterThanOrEqualTo(-1)
            .WithMessage("IndustryId must be -1 or greater.");

        // Validation for OrganizationText, ensuring the text length is 100 characters or less
        RuleFor(x => x.OrganizationText)
            .MaximumLength(100)
            .WithMessage("OrganizationText must be 100 characters or less.")
            .When(x => !string.IsNullOrEmpty(x.OrganizationText));

    }
}
