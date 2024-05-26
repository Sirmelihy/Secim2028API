using Secim2028.Dtos.OyAdayDto;
using Secim2028.Dtos.OyPartiDto;

namespace Secim2028.Services.OyServices
{
    public interface IOyPartiService
    {
        Task<List<OyPartiResponseDto>> GetOylar(int sandikNo);
        Task<List<OyPartiResponseDto>> AddOyParti(int sandikNo, int partiId, int OySayisi);

        Task<List<OyPartiResponseDto>> AddOyPartiJson(OyPartiRequestDto request);

        Task<string> ClearSandik(int sandikNo);

        Task<List<OyPartiResponseDto>> AddOyPartiRandom(int ilid, int partiId, int OySayisi);

    }
}
