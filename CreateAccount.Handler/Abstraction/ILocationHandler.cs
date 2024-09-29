using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Handler.Abstraction
{
    using CreateAccount.DTO.DTOs;

    public interface ILocationHandler
    {
        Task<List<LocationResponseDTO>> Handle(LocationRequestDTO request);
    }

}
