using CreateAccount.AggregateRoot;
using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using FluentValidation;

public class CheckNamesHandler : ICheckNamesHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IValidator<CheckNamesRequestDTO> _validator;

    public CheckNamesHandler(IUserRepository userRepository, ICompanyRepository companyRepository, IValidator<CheckNamesRequestDTO> validator)
    {
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        _validator = validator;
    }

    public async Task<CheckNamesResponseDTO> Handle(CheckNamesRequestDTO dto)
    {
        // Validate the request using FluentValidation
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            // Return validation errors as a message
            return new CheckNamesResponseDTO
            {
                Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            };
        }

        // Initialize the Aggregate Root and map DTO to the domain entities (manual mapping)
        var aggregateRoot = new AccountAggregateRoot(dto);

        // Check for existing user if 'CheckFor' is 'u'
        if (dto.CheckFor == "u" && aggregateRoot.User != null)
        {
            var userExists = await _userRepository.IsUserNameExistAsync(aggregateRoot.User.UserName);
            if (userExists)
            {
                return new CheckNamesResponseDTO { Message = "This Username already exists. Try another." };
            }
        }
        // Check for existing company if 'CheckFor' is 'c'
        else if (aggregateRoot.Company != null)
        {
            var companyExists = await _companyRepository.IsCompanyExistAsync(aggregateRoot.Company.Name);
            if (companyExists)
            {
                return new CheckNamesResponseDTO { Message = "Company Name already exists. Dial 16479 to retrieve your account." };
            }
        }

        // If everything is okay, return a success message
        return new CheckNamesResponseDTO { Message = "Success!" };
    }
}
