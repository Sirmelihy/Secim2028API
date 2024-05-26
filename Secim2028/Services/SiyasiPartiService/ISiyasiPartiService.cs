using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Services.SiyasiPartiService
{
    public interface ISiyasiPartiService
    {

        Task<List<SiyasiPartiResponseDto>> GetSiyasiPartiler();
        Task<List<SiyasiPartiResponseDto>> AddSiyasiPartis(List<SiyasiPartiRequestDto> request);
        Task<List<SiyasiPartiResponseDto>> DeleteSiyasiParti(int id);
        Task<ActionResult<SiyasiParti>> ChangeIttifak(int id, string ittifakName);
    }
}
