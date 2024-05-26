using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.OyAdayDto;
using Secim2028.Dtos.OyPartiDto;
using Secim2028.Services.OyServices;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OyPartiController : ControllerBase
    {
        private readonly IOyPartiService _service;
        public OyPartiController(IOyPartiService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OyPartiResponseDto>>> GetOylar(int sandikNo)
        {
            var value = await _service.GetOylar(sandikNo);

            if(value == null)
            {
                return BadRequest("hata");

            }

            return Ok(value);
        }

        [HttpPost , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<OyPartiResponseDto>>> AddOyParti(int sandikNo, int partiId, int OySayisi)
        {
            var value = await _service.AddOyParti(sandikNo, partiId, OySayisi);
            
            if(value == null )
            {
                return BadRequest("hata");
            }

            return Ok(value);


        }

        [HttpPost("AddJson") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<OyPartiResponseDto>>> AddOyPartiJson(OyPartiRequestDto request)
        {
            var value = await _service.AddOyPartiJson(request);

            if (value == null)
            {
                return BadRequest("hata");
            }

            return Ok(value);


        }

        [HttpPost("ClearSandik") , Authorize(Roles = "admin")]

        public async Task<ActionResult<string>> ClearAdaySandik(int sandikNo)
        {
            var value = await _service.ClearSandik(sandikNo);

            if (value == null)
            {
                return BadRequest($"{sandikNo} no'lu sandik bulunamadı");
            }

            return Ok(value);
        }

        [HttpPost("AddRandomSandikOy")]
        public async Task<ActionResult<List<OyPartiResponseDto>>> AddOyPartiRandom(int ilid, int partiId, int OySayisi)
        {
            var value = await _service.AddOyPartiRandom(ilid, partiId, OySayisi);
            if(value == null)
            {
                return BadRequest("hata");
            }

            return Ok(value);
        }



    }
}
