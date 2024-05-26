using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secim2028.Services.SehirService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecimIlController : ControllerBase
    {
        private readonly ISecimIlService _service;

        public SecimIlController(ISecimIlService service) {
            _service = service;

        }


        [HttpGet("GetIls")]
        public async Task<ActionResult<List<Il>>> GetIl ()
        {
            var value = await _service.GetIls ();
            return Ok(value);
        }

        [HttpGet("GetOnlyIls")]

        public async Task<ActionResult<List<Il>>> GetOnlyIls()
        {
            var value = await _service.GetOnlyIls();
            return Ok(value);
        }

        [HttpGet("{plaka}")]
        public async Task<ActionResult<Il>> GetIl(int plaka)
        {

            var value = await _service.GetIl(plaka);

            if(value == null)
            {
                return BadRequest($"Geçersiz plaka ({plaka})");
            }

            return Ok(value);
        }

        [HttpPost("ChangeIlName") , Authorize(Roles = "admin")]
        public async Task<ActionResult<Il>> ChangeIl(int plaka,string request)
        {

            var value = await _service.ChangeIl(plaka,request);
            if(value == null)
            {
                return BadRequest($"Geçersiz plaka ({plaka})");
            }

            return Ok(value);
        }

        [HttpPost("DeleteIl") , Authorize(Roles = "admin")]
        public async Task<ActionResult<Il>> DeleteIl (int plaka)
        {

            var value = await _service.DeleteIl(plaka);
            if(value == null)
            {
                return BadRequest($"Geçersiz Plaka ({plaka})");
            }
            return Ok(value);
        }

        [HttpPost("AddIl") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Il>>> PostIl (int plaka,string ilName)
        {

            var value = await _service.AddIl(plaka,ilName);
            return Ok(value);
        }

        [HttpPost("AddIller") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Il>>> AddIls(List<Il> iller)
        {
            var value = await _service.AddIls(iller);
            return Ok(value);
        }

    }
}
