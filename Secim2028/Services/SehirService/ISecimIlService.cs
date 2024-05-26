using Microsoft.AspNetCore.Mvc;

namespace Secim2028.Services.SehirService
{
    public interface ISecimIlService
    {

        Task<List<Il>> GetIls();

        Task<List<Il>> GetOnlyIls();

        Task<Il> GetIl(int plaka);

        Task<Il>? ChangeIl(int plaka, string request);

        Task<List<Il>?> DeleteIl(int plaka);

        Task<List<Il>> AddIl(int plaka, string ilName);

        Task<List<Il>> AddIls(List<Il> iller);


    }
}
