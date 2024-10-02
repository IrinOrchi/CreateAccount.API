using CreateAccount.AggregateRoot;
using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateAccount.Repository.Repository;
using FluentValidation;

namespace CreateAccount.Handler.Service
{


    public class RLNoHandler : IRLNoHandler
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IValidator<RLNoRequestDTO> _validator;

        public RLNoHandler(IGenericRepository genericRepository, IValidator<RLNoRequestDTO> validator)
        {
            _genericRepository = genericRepository;
            _validator = validator;
        }

        public async Task<RLNoResponseDTO> Handle(RLNoRequestDTO dto)
        {
            // Validate using FluentValidation
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                // Return validation errors
                return new RLNoResponseDTO
                {
                    Error = "1",
                    RL_no = "empty",
                    Company_Name = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
                };
            }

            // Fetch data from repository
            var data = await _genericRepository.GetRLNoDataAsync(dto.RLNo);

            // Return not found response if RLNo doesn't exist
            if (data == null)
            {
                return new RLNoResponseDTO
                {
                    Error = "0",
                    RL_no = "not found",
                    Company_Name = string.Empty
                };
            }

            // Return valid response if RLNo exists
            return new RLNoResponseDTO
            {
                Error = "0",
                RL_no = data.RL_no,
                Company_Name = data.Company_Name
            };
        }
    }

}
