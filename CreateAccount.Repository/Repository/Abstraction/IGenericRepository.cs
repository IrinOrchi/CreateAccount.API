using CreateAccount.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Repository.Repository.Abstraction
{
    public interface IGenericRepository
    {
        Task<bool> IsUserNameExistAsync(string userName);
        Task<bool> IsCompanyExistAsync(string companyName);
        Task<List<LocationResponseDTO>> GetLocationsAsync(string districtId, string outsideBd);
Task<List<IndustryTypeResponseDTO>> GetIndustryTypesAsync(int? industryId, string organizationText = null, int? corporateID = null);


    }
}
