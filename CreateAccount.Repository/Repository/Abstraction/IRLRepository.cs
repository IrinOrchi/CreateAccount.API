using CreateAccount.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Repository.Repository.Abstraction
{
    public interface IRLRepository
    {
        Task<RLNoDataDTO> GetRLNoDataAsync(string rlNo);
    }

}
