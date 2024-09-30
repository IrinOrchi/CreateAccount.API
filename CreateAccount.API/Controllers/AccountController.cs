using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreateAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICheckNamesHandler _checkNamesHandler;

        public AccountController(ICheckNamesHandler checkNamesHandler)
        {
            _checkNamesHandler = checkNamesHandler;
        }

        // POST method for checking names
        [HttpPost("CheckNames")]
        public async Task<IActionResult> CheckNames([FromBody] CheckNamesRequestDTO request)
        {
            var response = await _checkNamesHandler.Handle(request);
            if (!string.IsNullOrEmpty(response.Message))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // NEW: Check if both UserName and CompanyName exist (GET method)
        [HttpGet("CheckUserAndCompany")]
        public async Task<IActionResult> CheckUserAndCompany([FromQuery] string userName, [FromQuery] string companyName)
        {
            var checkRequest = new CheckNamesRequestDTO
            {
                UserName = userName,
                CompanyName = companyName
            };

            var response = await _checkNamesHandler.Handle(checkRequest);
            if (!string.IsNullOrEmpty(response.Message))
            {
                return BadRequest(response);
            }

            return Ok(new
            {
                UserExists = string.IsNullOrEmpty(checkRequest.UserName) ? false : await _checkNamesHandler.UserExistsAsync(checkRequest.UserName),
                CompanyExists = string.IsNullOrEmpty(checkRequest.CompanyName) ? false : await _checkNamesHandler.CompanyExistsAsync(checkRequest.CompanyName)
            });
        }
    }
}
