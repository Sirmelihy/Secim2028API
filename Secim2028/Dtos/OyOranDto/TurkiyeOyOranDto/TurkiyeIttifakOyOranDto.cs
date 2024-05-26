using Secim2028.Dtos.IttifakDto;

namespace Secim2028.Dtos.OyOranDto.TurkiyeOyOranDto
{
    public class TurkiyeIttifakOyOranDto
    {


        public double OyOrani { get; set; }

        public string yuzdeOyOrani { get; set; }
        public int oySayisi { get; set; }

        public IttifakResponseOfOyOranDto Ittifak { get; set; }


    }
}
