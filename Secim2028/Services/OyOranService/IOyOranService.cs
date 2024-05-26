using Secim2028.Dtos.OyOranDto.IlceOyOranDto;
using Secim2028.Dtos.OyOranDto.IlOyOranDto;
using Secim2028.Dtos.OyOranDto.TurkiyeOyOranDto;
using Secim2028.Dtos.OyOranDto.WinnerAdaysDto;
using Secim2028.Dtos.OyOranDto.WinnerIlTimesDto;

namespace Secim2028.Services.OyOranService
{
    public interface IOyOranService
    {

        Task<List<IlAdayOyOranDto>> GetIlAdayOyOran(int ilid);

        Task<List<IlceAdayOranDto>> GetIlceAdayOran(int ilceid);

        Task<List<IlPartiOyOranDto>> GetIlPartiOyOran(int ilid);

        Task<List<IlcePartiOyOranDto>> GetIlcePartiOyOran(int ilceid);

        Task<List<TurkiyeAdayOyOranDto>> GetTurkiyeAdayOyOran();

        Task<List<TurkiyePartiOyOranDto>> GetTurkiyePartiOran();

        Task<List<TurkiyeIttifakOyOranDto>> GetTurkiyeIttifakOyOran();

        Task<List<WinnerAdayDto>> GetWinnerAdays();
        Task<List<WinnerPartiDto>> GetWinnerPartis();


        Task<List<IlAdayOyOranDto>> GetTurkiyeAdayOranEachIl();
        Task<List<IlPartiOyOranDto>> GetTurkiyePartiOranEachIl();

        Task<int> PartiKullanılanOy();
        Task<int> AdayKullanılanOy();

    }
}
