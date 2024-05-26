using Secim2028.Dtos.AdayDto;

namespace Secim2028.Dtos.OyOranDto.IlOyOranDto
{
    public class IlAdayOyOranDto
    {

        public int ilID { get; set; }
        public string ilAdi { get; set; }
        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }
        public int oySayisi { get; set; }

        public AdayResponseForOyAdayDto aday { get; set; }

        



    }
}
