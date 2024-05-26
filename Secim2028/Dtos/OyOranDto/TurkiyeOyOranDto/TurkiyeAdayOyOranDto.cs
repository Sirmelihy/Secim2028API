using Secim2028.Dtos.AdayDto;

namespace Secim2028.Dtos.OyOranDto.TurkiyeOyOranDto
{
    public class TurkiyeAdayOyOranDto
    {
        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }
        public int oySayisi { get; set; }

        public AdayResponseForOyAdayDto aday { get; set; }

    }
}
