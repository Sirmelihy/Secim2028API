using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secim2028.Models
{
    public class OyParti
    {
        [Key]
        public int OyId { get; set; }

        [ForeignKey("SandikId")]
        public Sandik Sandik { get; set; }

        [ForeignKey("SiyasiPartiId")]
        public SiyasiParti SiyasiParti { get; set; }

        public int OySayisi { get; set; }
    }
}
