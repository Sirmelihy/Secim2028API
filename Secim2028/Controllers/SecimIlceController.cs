using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secim2028.Services.IlceService;

namespace Secim2028.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecimIlceController : ControllerBase
    {

        private readonly ISecimIlceService _service;
        public SecimIlceController(ISecimIlceService service) { 
        
            _service = service;

        }


        [HttpGet("GetIlces")]
        public async Task<ActionResult<List<ilceResponseDto>>> GetIlces()
        {
            var returner = await _service.GetIlces();
            return Ok(returner);

        }

        [HttpGet("GetOnlyIlces")]
        public async Task<ActionResult<List<Ilce>>> GetOnlyIlces()
        {
            var returner = await _service.GetOnlyIlces();
            return Ok(returner);

        }

        [HttpPost , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Ilce>>> AddIlces(List<IlceRequestDto> ilceList)
        {
            var returner = await _service.AddIlces(ilceList);
            return Ok(returner);
        }

        [HttpPost("DeleteIlce") , Authorize(Roles = "admin")]
        public async Task<ActionResult<List<Ilce>>> DeleteIlce(int id)
        {
            var value = await _service.DeleteIlce(id);

            if(value == null)
            {
                return BadRequest($"Geçersiz ilce id ({id})");
            }

            return Ok(value);
        }







    }
}
