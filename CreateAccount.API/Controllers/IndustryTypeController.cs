using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace IndustryTypePrac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryTypeController : ControllerBase
    {
        private readonly IIndustryTypeHandler _industryTypeHandler;

        public IndustryTypeController(IIndustryTypeHandler industryTypeHandler)
        {
            _industryTypeHandler = industryTypeHandler;
        }

        [HttpPost("GetIndustryType")]
        public async Task<IActionResult> GetIndustryTypeAsync(IndustryTypeRequestDTO request)
        {
            try
            {
                var industryTypes = await _industryTypeHandler.HandleIndustryTypeAsync(request);

                var response = new
                {
                    Error = "0",
                    IndustryType = industryTypes.Any() ? (object)industryTypes : null 
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Error = "1",
                    Message = ex.Message
                };

                return BadRequest(errorResponse);
            }
        }

    }
}
