using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.OyOranDto.IlceOyOranDto;
using Secim2028.Dtos.OyOranDto.IlOyOranDto;
using Secim2028.Dtos.OyOranDto.TurkiyeOyOranDto;
using Secim2028.Dtos.OyOranDto.WinnerAdaysDto;
using Secim2028.Dtos.OyOranDto.WinnerIlTimesDto;
using Secim2028.Services.OyOranService;
using System.Runtime.InteropServices;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OylarController : ControllerBase
    {
        private readonly IOyOranService _service;
        public OylarController(IOyOranService service)
        {
            _service = service;
        }


        [HttpGet("GetIlAdayOyOran")]
        public async Task<ActionResult<List<IlAdayOyOranDto>>> GetIlAdayOyOran(int ilid)
        {
            var value = await _service.GetIlAdayOyOran(ilid);
            if(value == null)
            {
                return BadRequest("Oy bulunamadı");
            }
            return Ok(value);
        }

        [HttpGet("GetIlceAdayOyOran")]
        public async Task<ActionResult<List<IlceAdayOranDto>>> GetIlceAdayOran(int ilceid)
        {
            var value = await _service.GetIlceAdayOran(ilceid);
            return Ok(value);

        }

        [HttpGet("GetIlPartiOyOran")]
        public async Task<ActionResult<List<IlPartiOyOranDto>>> GetIlPartiOyOran(int ilid)
        {
            var value = await _service.GetIlPartiOyOran(ilid);
            return Ok(value);
        }

        [HttpGet("GetIlcePartiOyOran")]
        public async Task<ActionResult<List<IlcePartiOyOranDto>>> GetIlcePartiOyOran (int ilceid)
        {
            var value = await _service.GetIlcePartiOyOran(ilceid);
            return Ok(value);
        }

        [HttpGet("GetTurkiyeAdayOyOran")]
        public async Task<ActionResult<List<TurkiyeAdayOyOranDto>>> GetTurkiyeAdayOyOran()
        {
            var value = await _service.GetTurkiyeAdayOyOran();
            return Ok(value);
        }

        [HttpGet("GetTurkiyePartiOran")]
        public async Task<ActionResult<List<TurkiyePartiOyOranDto>>> GetTurkiyePartiOran()
        {
            var value = await _service.GetTurkiyePartiOran();
            return Ok(value);
        }

        [HttpGet("GetTurkiyeIttifakOran")]
        public async Task<ActionResult<List<TurkiyeIttifakOyOranDto>>> GetTurkiyeIttifakOyOran()
        {
            var value = await _service.GetTurkiyeIttifakOyOran();
            return Ok(value);
        }

        [HttpGet("ToplamPartiOy")]
        public async Task<ActionResult<int>> PartiKullanılanOy()
        {
            var value = await _service.PartiKullanılanOy();
            return Ok(value);
        }

        [HttpGet("ToplamAdayOy")]
        public async Task<ActionResult<int>> AdayKullanılanOy()
        {
            var value = await _service.AdayKullanılanOy();
            return Ok(value);
        }

        [HttpGet("GetTurkiyeAdayOranEachIl")]
        public async Task<ActionResult<List<IlAdayOyOranDto>>> GetTurkiyeAdayOranEachIl()
        {
            var value = await _service.GetTurkiyeAdayOranEachIl();
            return Ok(value);

        }

        [HttpGet("GetTurkiyePartiOranEachIl")]
        public async Task<ActionResult<List<IlPartiOyOranDto>>> GetTurkiyePartiOranEachIl()
        {
            var value = await _service.GetTurkiyePartiOranEachIl();
            return Ok(value);
        }

        [HttpGet("AdayWinTimesOfIl")]
        public async Task<ActionResult<List<WinnerAdayDto>>> GetWinnerAdays()
        {
            var value = await _service.GetWinnerAdays();
            return Ok(value);
        }

        [HttpGet("PartiWinOfTimesIl")]
        public async Task<ActionResult<List<WinnerPartiDto>>> GetWinnerPartis()
        {
            var value = await _service.GetWinnerPartis();
            return Ok(value);
        }

    }
}
