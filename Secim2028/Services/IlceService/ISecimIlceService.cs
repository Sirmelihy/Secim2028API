using Secim2028.Models;

namespace Secim2028.Services.IlceService

{
    public interface ISecimIlceService
    {
        Task<List<ilceResponseDto>> GetIlces();
        Task<List<Ilce>> GetOnlyIlces();

        Task<List<Ilce>> AddIlces(List<IlceRequestDto> ilceList);

        Task<List<Ilce>> DeleteIlce(int id);




    }
}
