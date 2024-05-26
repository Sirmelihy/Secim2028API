using System.ComponentModel.DataAnnotations.Schema;

namespace Secim2028.Dtos
{
    public class ilceResponseDto
    {
        public int IlceId { get; set; }
        public string IlceAdi { get; set; }
        public IlDto Il { get; set; }
    }
}
