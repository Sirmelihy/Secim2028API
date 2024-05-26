using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Dtos.AdayDto
{
    public class AdayResponseDto
    {

        public int AdayId { get; set; }

        public string AdayAdi { get; set; }

        public SiyasiPartiResponseDto SiyasiParti { get; set;}


    }
}
