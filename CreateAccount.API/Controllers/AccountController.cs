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
    }
}
