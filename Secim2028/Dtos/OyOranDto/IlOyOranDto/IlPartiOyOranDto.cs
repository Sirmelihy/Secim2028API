using Secim2028.Dtos.AdayDto;
using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Dtos.OyOranDto.IlOyOranDto
{
    public class IlPartiOyOranDto
    {

        public int ilID { get; set; }
        public string ilAdi { get; set; }
        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }
        public int oySayisi { get; set; }

        public SiyasiPartiForOyPartiDto SiyasiParti { get; set; }

    }
}
