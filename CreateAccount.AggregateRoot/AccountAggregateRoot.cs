using CreateAccount.AggregateRoot.Entities;
using CreateAccount.DTO.DTOs;

public class AccountAggregateRoot
{
    public User User { get; private set; }
    public Company Company { get; private set; }

    public AccountAggregateRoot(CheckNamesRequestDTO dto)
    {
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
}
