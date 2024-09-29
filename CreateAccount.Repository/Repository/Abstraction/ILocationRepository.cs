using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Repository.Repository.Abstraction
{
    using CreateAccount.DTO.DTOs;

    public interface ILocationRepository
    {
        Task<List<LocationResponseDTO>> GetLocationsAsync(string districtId, string outsideBd);
    }

}
