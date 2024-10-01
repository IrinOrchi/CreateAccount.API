using CreateAccount.Handler.Abstraction;
using CreateAccount.Handler.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateAccount.API.Controllers
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
        public async Task<IActionResult> GetIndustryType([FromBody] IndustryTypeRequestDTO request)
        {
            var industryTypes = await _industryTypeHandler.HandleIndustryType(request);

            if (industryTypes.Count == 0)
            {
                return Ok(new { Error = "0", IndustryType = "null" });
            }

            return Ok(new
            {
                Error = "0",
                IndustryType = industryTypes
            });
        }


    }
}
