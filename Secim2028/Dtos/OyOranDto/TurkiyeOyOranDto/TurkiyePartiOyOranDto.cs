using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Dtos.OyOranDto.TurkiyeOyOranDto
{
    public class TurkiyePartiOyOranDto
    {
        public int id {  get; set; }
        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }
        public int oySayisi { get; set; }

        public SiyasiPartiForOyPartiDto SiyasiParti { get; set; }
    }
}
