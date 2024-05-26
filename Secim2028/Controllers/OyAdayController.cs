using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.OyAdayDto;
using Secim2028.Models;
using Secim2028.Services.OyServices;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OyAdayController : ControllerBase
    {

        private readonly IOyAdayService _service;
        public OyAdayController(IOyAdayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OyAdayResponseDto>>> GetOylar(int sandikNo)
        {
            var value = await _service.GetOylar(sandikNo);
            return Ok(value);

        }

        [HttpPost , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<OyAdayResponseDto>>> AddOyAday(int sandikNo, int adayId, int OySayisi)
        {
            var value = await _service.AddOyAday(sandikNo, adayId, OySayisi);

            if(value == null)
            {
                return BadRequest("hata");
            }

            return Ok(value);
        }

        [HttpPost("PostList") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<OyAdayResponseDto>>> AddOyAdayJson(OyAdayRequestDto request)
        {
            var value = await _service.AddOyAdayJson(request);

            if (value == null)
            {
                return BadRequest("hata");
            }

            return Ok(value);

        }

        [HttpPost("ClearSandik"), Authorize(Roles = "admin")]

        public async Task<ActionResult<string>> ClearAdaySandik(int sandikNo)
        {
            var value = await _service.ClearSandik(sandikNo);

            if(value == null) {
                return BadRequest($"{sandikNo} no'lu sandik bulunamadı");
            }

            return Ok(value);
        }

        [HttpPost("AddRandomSandikOy")]
        public async Task<ActionResult<List<OyAdayResponseDto>>> AddOyAdayRandom(int ilid, int adayId, int OySayisi)
        {
            var value = await _service.AddOyAdayRandom(ilid, adayId, OySayisi);
            return Ok(value);
        }

    }
}
