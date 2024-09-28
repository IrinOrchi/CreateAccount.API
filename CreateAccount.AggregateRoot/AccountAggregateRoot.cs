using CreateAccount.AggregateRoot.Entities;
using CreateAccount.DTO.DTOs;

public class AccountAggregateRoot
{
    public User User { get; private set; }
    public Company Company { get; private set; }

    // Property to hold any validation messages
    public string ValidationMessage { get; private set; }

    // Constructor to initialize User and Company entities
    public AccountAggregateRoot(CheckNamesRequestDTO dto)
    {
        ValidationMessage = string.Empty;

        if (!string.IsNullOrEmpty(dto.UserName))
        {
            User = new User
            {
                UserName = dto.UserName.Trim().Replace("'", "`")
            };
        }

        if (!string.IsNullOrEmpty(dto.CompanyName))
        {
            Company = new Company
            {
                Name = dto.CompanyName.Trim().Replace("'", "`")
            };
        }
    }

    // Validation method to ensure User or Company data is valid
    public bool IsValid()
    {
        if (User != null && (User.UserName.Length < 3 || User.UserName.Length > 20))
        {
            ValidationMessage = "Username must be between 3 and 20 characters.";
            return false;
        }

        if (Company != null && string.IsNullOrWhiteSpace(Company.Name))
        {
            ValidationMessage = "Company name cannot be empty.";
            return false;
        }

        return true;
    }

    // Example of a method that builds a company name query
    public string BuildCompanyNameQuery()
    {
        if (Company == null) return null;

        return "SELECT CP_ID, count(1) over() FROM dbo_Company_Profiles" +
               " WHERE (OfflineCom IS NULL OR OfflineCom = 0) AND lower(replace(replace(Name, ' Ltd', ''), ' Pvt', ''))" +
               $" = lower(replace(replace('{Company.Name}', ' Ltd', ''), ' Pvt', ''))";
    }
}
