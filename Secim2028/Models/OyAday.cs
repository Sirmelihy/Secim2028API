using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secim2028.Models
{
    public class OyAday
    {
        [Key]
        public int OyId { get; set; }

        [ForeignKey("SandikId")]
        public Sandik Sandik { get; set; }

        [ForeignKey("AdayId")]
        public AdayCumhurbaskani CumhurbaskaniAdayi { get; set; }

        public int OySayisi { get; set; }
    }
}
