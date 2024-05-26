using Secim2028.Dtos.AdayDto;

namespace Secim2028.Dtos.OyOranDto.IlceOyOranDto
{
    public class IlceAdayOranDto
    {
        public int ilceID { get; set; }
        public string ilceAdi { get; set; }
        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }

        public AdayResponseForOyAdayDto aday { get; set; }

    }
}
