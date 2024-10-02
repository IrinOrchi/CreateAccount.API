using CreateAccount.DTO.DTOs;
using FluentValidation;

public class RLDTOValidator : AbstractValidator<RLNoRequestDTO>
{
    public RLDTOValidator()
    {
        // RLNo is required and must be numeric
        RuleFor(x => x.RLNo)
            .NotEmpty().WithMessage("RLNo is required.")
            .Must(IsNumeric).WithMessage("RLNo must be numeric.");
    }

    // Helper method to check if the RLNo is numeric
    private bool IsNumeric(string rlNo)
    {
        return int.TryParse(rlNo, out _);
    }
}
