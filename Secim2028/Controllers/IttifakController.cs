using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.IttifakDto;
using Secim2028.Services.IttifakService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IttifakController : ControllerBase
    {
        private readonly IIttifakService _service;
        public IttifakController(IIttifakService service) {

            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<IttifakResponseDto>>> GetIttifaks()
        {
            var value = await _service.GetIttifaks();
            return Ok(value);
        }

        [HttpPost("AddIttifak") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<IttifakResponseDto>>> AddIttifak (IttifakRequestDto request)
        {
            var value = await _service.AddIttifak(request);
            return Ok(value);

        }


        [HttpPost("DeleteIttifak") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<IttifakResponseDto>>> DeleteIttifak(int ittifakid)
        {
            var value = await _service.DeleteIttifak(ittifakid);

            if(value == null)
            {
                return NotFound($"Bu id'ye ({ittifakid}) sahip bir ittifak bulunamadı");
            }

            return Ok(value);
        }
    }
}
