using Secim2028.Dtos.OyAdayDto;

namespace Secim2028.Services.OyServices
{
    public interface IOyAdayService
    {

        Task<List<OyAdayResponseDto>> AddOyAday(int sandikNo,int adayId,int OySayisi);

        Task<List<OyAdayResponseDto>> AddOyAdayJson(OyAdayRequestDto request);

        Task<List<OyAdayResponseDto>> GetOylar (int sandikNo);

        Task<string> ClearSandik(int sandikNo);

        Task<List<OyAdayResponseDto>> AddOyAdayRandom(int ilid, int adayId, int OySayisi);

    }
}
