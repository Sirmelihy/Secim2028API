using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secim2028.Models
{
    public class SiyasiParti
    {
        [Key]
        public int SiyasiPartiId { get; set; }
        public string SiyasiPartiAdi { get; set; }

        public string SiyasiPartiKisaltma { get; set; }

        [ForeignKey("IttifakId")]
        public Ittifak? Ittifak { get; set; }

        public List<AdayCumhurbaskani> CumhurbaskaniAdayi { get; set; }




    }
}
