using Secim2028.Dtos.SandikDto;

namespace Secim2028.Services.SandikService
{
    public interface ISecimSandikService
    {

        Task<List<SandikResponseDto>> GetSandiks(int ilid);
        Task<List<SandikResponseDto>> AddSandiks(List<SandikCycleDto> request);
        Task<SandikFirstAndLastResponseDto> GetFirstAndLastSandik(int ilid);

    }
}
