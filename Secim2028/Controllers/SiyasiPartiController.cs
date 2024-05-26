using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secim2028.Dtos.SiyasiPartiDto;
using Secim2028.Services.SiyasiPartiService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiyasiPartiController : ControllerBase
    {

        private readonly ISiyasiPartiService _service;
        public SiyasiPartiController(ISiyasiPartiService service) {
        
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<SiyasiPartiResponseDto>>> GetSiyasiPartiler()
        {
            var value = await _service.GetSiyasiPartiler();
            return Ok(value);
        }


        [HttpPost("AddSiyasiPartis") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<SiyasiPartiResponseDto>>> AddSiyasiPartis(List<SiyasiPartiRequestDto> request)
        {
            var value = await _service.AddSiyasiPartis(request);
            return Ok(value);
        }



        [HttpPost("DeleteSiyasiParti") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<SiyasiPartiResponseDto>>> DeleteSiyasiParti(int id)
        {
            var value = await _service.DeleteSiyasiParti(id);

            if(value != null) {
                return NotFound($"Belirtilen id'ye ({id}) ait bir sonuç bulunamadı!!!");
            }

            return Ok(value);

        }

        [HttpPost("ChangeIttifak") , Authorize(Roles = "admin")]
        public async Task<ActionResult<SiyasiParti>> ChangeIttifak (int id, string ittifakName)
        {
            var value = await _service.ChangeIttifak(id,ittifakName);

            if (value == null)
            {
                return NotFound($"Belirtilen id'ye ({id}) ait parti bulunamadı");
            }

            return Ok(value);
        }






    }
}
