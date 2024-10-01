using CreateAccount.DTO.DTOs;
using FluentValidation;

public class IndustryTypeRequestDTOValidator : AbstractValidator<IndustryTypeRequestDTO>
{
    public IndustryTypeRequestDTOValidator()
    {
        RuleFor(x => x.IndustryId)
            .GreaterThanOrEqualTo(-1).WithMessage("IndustryId must be -1 or greater.");

        RuleFor(x => x.OrganizationText)
            .MaximumLength(100).WithMessage("OrganizationText must be 100 characters or less.");

        RuleFor(x => x.CorporateID)
            .GreaterThan(0).When(x => x.CorporateID.HasValue).WithMessage("CorporateID must be greater than 0.");
    }
}
