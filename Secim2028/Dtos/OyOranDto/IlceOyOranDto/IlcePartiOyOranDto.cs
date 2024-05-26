using Secim2028.Dtos.AdayDto;
using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Dtos.OyOranDto.IlceOyOranDto
{
    public class IlcePartiOyOranDto
    {
        public int ilceID { get; set; }
        public string ilceAdi { get; set; }
        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }

        public SiyasiPartiForOyPartiDto SiyasiParti { get; set; }

    }
}
