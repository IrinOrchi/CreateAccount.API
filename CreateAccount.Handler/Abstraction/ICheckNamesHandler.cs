using CreateAccount.DTO.DTOs;
using System.Threading.Tasks;

namespace CreateAccount.Handler.Abstraction
{
    public interface ICheckNamesHandler
    {
        Task<CheckNamesResponseDTO> Handle(CheckNamesRequestDTO dto);

        // New methods for checking user and company existence
        Task<bool> UserExistsAsync(string userName);
        Task<bool> CompanyExistsAsync(string companyName);
    }
}
