using Secim2028.Dtos.IttifakDto;

namespace Secim2028.Dtos.SiyasiPartiDto
{
    public class SiyasiPartiResponseDto
    {

        public int SiyasiPartiId { get; set; }
        public string SiyasiPartiAdi { get; set; }

        public string SiyasiPartiKisaltma { get; set; }

        public IttifakResponseOfSiyasiPartiDto? Ittifak { get; set; }



    }
}
