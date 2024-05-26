using Secim2028.Dtos.OyOranDto.IlOyOranDto;

namespace Secim2028.Helper.FirstPartiHelper
{
    public interface ITurkiyeFirstPartiEachIlHelper
    {
        Task<List<IlPartiOyOranDto>> GetTurkiyePartiOranEachIl();
    }
}
