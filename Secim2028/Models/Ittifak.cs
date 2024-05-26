using System.ComponentModel.DataAnnotations;

namespace Secim2028.Models
{
    public class Ittifak
    {
        [Key]
        public int IttifakId { get; set; }

        public string IttifakAdi { get; set; }

        public List<SiyasiParti> SiyasiPartiler { get; set; }
    }
}
