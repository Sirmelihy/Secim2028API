using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Dtos.IttifakDto
{
    public class IttifakResponseDto
    {
        public int IttifakId { get; set; }

        public string IttifakAdi { get; set; }

        public List<SiyasiPartiResponseOfIttifakDto> SiyasiPartis { get; set; }


    }
}
