using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using FluentValidation;

namespace CreateAccount.Handler.Service
{
    public class IndustryTypeHandler : IIndustryTypeHandler
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IValidator<IndustryTypeRequestDTO> _validator;

        public IndustryTypeHandler(IGenericRepository genericRepository, IValidator<IndustryTypeRequestDTO> validator)
        {
            _genericRepository = genericRepository;
            _validator = validator;
        }

        public async Task<List<IndustryTypeResponseDTO>> HandleIndustryTypeAsync(IndustryTypeRequestDTO request)
        {
            // Validate the request DTO
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Call repository method to fetch the data
            var industryTypes = await _genericRepository.GetIndustryTypesAsync(request.IndustryId, request.OrganizationText, request.CorporateID);
            return industryTypes;
        }
    }
}
