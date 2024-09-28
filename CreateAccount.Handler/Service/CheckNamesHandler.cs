using CreateAccount.AggregateRoot;
using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;

public class CheckNamesHandler : ICheckNamesHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;

    public CheckNamesHandler(IUserRepository userRepository, ICompanyRepository companyRepository)
    {
        _userRepository = userRepository;
        _companyRepository = companyRepository;
    }

    public async Task<CheckNamesResponseDTO> Handle(CheckNamesRequestDTO dto)
    {
        // Initialize the Aggregate Root
        var aggregateRoot = new AccountAggregateRoot(dto);

        // Validate using the Aggregate Root
        if (!aggregateRoot.IsValid())
        {
            // Return the validation message if validation fails
            return new CheckNamesResponseDTO { Message = aggregateRoot.ValidationMessage };
        }

        if (dto.CheckFor == "u" && aggregateRoot.User != null)
        {
            var userExists = await _userRepository.IsUserNameExistAsync(aggregateRoot.User.UserName);
            if (userExists)
            {
                return new CheckNamesResponseDTO { Message = "This Username already exists. Try another." };
            }
        }
        else if (aggregateRoot.Company != null)
        {
            var companyExists = await _companyRepository.IsCompanyExistAsync(aggregateRoot.Company.Name);
            if (companyExists)
            {
                return new CheckNamesResponseDTO { Message = "Company Name already exists. Dial 16479 to retrieve your account." };
            }
        }

        return new CheckNamesResponseDTO { Message = "" };
    }
}
