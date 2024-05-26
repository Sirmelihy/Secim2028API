using Microsoft.AspNetCore.Mvc;
using Secim2028.Dtos.AdayDto;

namespace Secim2028.Services.AdayService
{
    public interface ISecimAdayService
    {

        Task<List<AdayResponseDto>> GetAdays();
        Task<List<AdayResponseDto>> AddAdays(List<AdayRequestDto> request);
        Task<AdayResponseDto> ChangeAdayName(int id,string name);
        Task<List<AdayResponseDto>> DeleteAday(int id);



    }
}
