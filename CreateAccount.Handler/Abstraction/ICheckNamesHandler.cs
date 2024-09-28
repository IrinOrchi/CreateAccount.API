using CreateAccount.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Handler.Abstraction
{
    public interface ICheckNamesHandler
    {
        Task<CheckNamesResponseDTO> Handle(CheckNamesRequestDTO dto);
    }

}
