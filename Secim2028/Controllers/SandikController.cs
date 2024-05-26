using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.SandikDto;
using Secim2028.Services.SandikService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandikController : ControllerBase
    {

        private readonly ISecimSandikService _service;
        public SandikController(ISecimSandikService service)
        {

            _service = service;

        }

        [HttpGet("GetAllSandiks") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<SandikResponseDto>>> GetSandiks(int ilid) {

            var value = await _service.GetSandiks(ilid);
            return Ok(value);
        }

        [HttpPost("AddSandiks") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<SandikResponseDto>>> AddSandiks(List<SandikCycleDto> request)
        {
            var value = await _service.AddSandiks(request);

            if(value == null)
            {
                return BadRequest("Hata!");
            }

            return Ok(value);
        }

        [HttpGet("FirstAndLasst")]
        public async Task<ActionResult<SandikFirstAndLastResponseDto>> GetSandikFirstAndLast(int ilid)
        {
            var value = await _service.GetFirstAndLastSandik(ilid);
            return Ok(value);
        }
    }
}
