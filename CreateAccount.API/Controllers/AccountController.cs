using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Repository.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CreateAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICheckNamesHandler _checkNamesHandler;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository; // Add company repository for checking

        public AccountController(ICheckNamesHandler checkNamesHandler, IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _checkNamesHandler = checkNamesHandler;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        // Existing POST method for checking names
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
            // Check if the username exists
            var userExists = await _userRepository.IsUserNameExistAsync(userName);

            // Check if the company name exists
            var companyExists = await _companyRepository.IsCompanyExistAsync(companyName);

            // Return result as JSON
            return Ok(new
            {
                UserExists = userExists,
                CompanyExists = companyExists
            });
        }
    }
}
