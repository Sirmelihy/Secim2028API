using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.UserDto;
using Secim2028.Services.AuthService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var value = await _service.Register(request);
            return Ok(value);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var value = await _service.Login(request);

            if(value == null)
            {
                return BadRequest("Hatalı veya eksik bilgi");
            }

            return Ok(value);
        }

    }
}
