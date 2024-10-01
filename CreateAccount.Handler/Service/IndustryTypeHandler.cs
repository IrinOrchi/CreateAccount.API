using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccount.Handler.Service
{
    public class IndustryTypeHandler: IIndustryTypeHandler
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IValidator<CheckNamesRequestDTO> _validator;

        public IndustryTypeHandler(IGenericRepository genericRepository, IValidator<CheckNamesRequestDTO> validator)
        {
            _genericRepository = genericRepository;
            _validator = validator;
        }
        public async Task<List<IndustryTypeResponseDTO>> HandleIndustryType(IndustryTypeRequestDTO request)
        {
            var industryTypes = await _genericRepository.GetIndustryTypeAsync(request.IndustryId, request.OrganizationText, request.CorporateID);
            return industryTypes;
        }

    }
}
