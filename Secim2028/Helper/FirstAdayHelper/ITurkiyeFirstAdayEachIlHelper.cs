using Secim2028.Dtos.OyOranDto.IlOyOranDto;

namespace Secim2028.Helper.FirstAdayHelper
{
    public interface ITurkiyeFirstAdayEachIlHelper
    {

        Task<List<IlAdayOyOranDto>> GetTurkiyeAdayOranEachIl();
    }
}
