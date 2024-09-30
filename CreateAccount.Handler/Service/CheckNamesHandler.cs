using CreateAccount.AggregateRoot;
using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using FluentValidation;
using System.Threading.Tasks;

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

        var aggregateRoot = new AccountAggregateRoot(dto);
        if (dto.CheckFor == "c")
        {
            if (!string.IsNullOrEmpty(dto.CompanyName))
            {
                var companyExists = await _companyRepository.IsCompanyExistAsync(dto.CompanyName);
                if (companyExists)
                {
                    return new CheckNamesResponseDTO { Message = "Company Name already exists. Dial 16479 to retrieve your account." };
                }
            }
        }
        else if (dto.CheckFor == "u")
        {
            if (!string.IsNullOrEmpty(dto.UserName))
            {
                var userExists = await _userRepository.IsUserNameExistAsync(dto.UserName);
                if (userExists)
                {
                    return new CheckNamesResponseDTO { Message = "This Username already exists. Try another." };
                }
            }

        }
        return new CheckNamesResponseDTO { Message = "Success!" };
    }

    public async Task<bool> UserExistsAsync(string userName)
    {
        return await _userRepository.IsUserNameExistAsync(userName);
    }

    public async Task<bool> CompanyExistsAsync(string companyName)
    {
        return await _companyRepository.IsCompanyExistAsync(companyName);
    }
}
