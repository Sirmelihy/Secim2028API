using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secim2028.Dtos.AdayDto;
using Secim2028.Services.AdayService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdayController : ControllerBase
    {

        private readonly ISecimAdayService _service;
        public AdayController(ISecimAdayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdayResponseDto>>> GetAdays()
        {
            var value = await _service.GetAdays();

            return Ok(value);

        }

        [HttpPost("AddAdays") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<AdayResponseDto>>> AddAdays(List<AdayRequestDto> request)
        {

            var value = await _service.AddAdays(request);

            if(value == null)
            {
                return BadRequest("Veri girişinde bir hata oldu");
            }

            return Ok(value);

        }

        [HttpPost("ChangeAday") , Authorize(Roles = "admin")]
        public async Task<ActionResult<AdayResponseDto>> ChangeAday(int id,string name)
        {
            var value = await _service.ChangeAdayName(id,name);

            if(value == null)
            {
                return BadRequest($"ID ({id}) is not valid");
            }

            return Ok(value);
        }


        [HttpPost("DeleteAday") , Authorize(Roles = "admin")]

        public async Task<ActionResult<List<AdayResponseDto>>> DeleteAday (int id)
        {
            var value = await _service.DeleteAday(id);

            if(value == null)
            {
                return BadRequest($"Belirtilen id({id}) aday bulunamadı");
            }

            return Ok(value);

        }





    }
}
