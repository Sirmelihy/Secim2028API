using Secim2028.Dtos.IttifakDto;

namespace Secim2028.Services.IttifakService
{
    public interface IIttifakService
    {
        Task<List<IttifakResponseDto>> GetIttifaks();

        Task<List<IttifakResponseDto>> AddIttifak (IttifakRequestDto request);

        Task<List<IttifakResponseDto>> DeleteIttifak(int ittifakid);
    }
}
