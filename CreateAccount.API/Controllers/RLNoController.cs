using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CreateAccount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RLNoController : ControllerBase
    {
        private readonly IRLNoHandler _rlNoHandler;

        public RLNoController(IRLNoHandler rlNoHandler)
        {
            _rlNoHandler = rlNoHandler;
        }

        [HttpPost("CheckRLNo")]
        public async Task<IActionResult> CheckRLNo([FromBody] RLNoRequestDTO request)
        {
            var response = await _rlNoHandler.Handle(request);
            return Ok(response);
        }
    }

}
