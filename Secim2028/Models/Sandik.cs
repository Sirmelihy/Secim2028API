using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secim2028.Models
{
    public class Sandik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SandikId { get; set; }
        public int SandikNo { get; set; }

        [ForeignKey("IlceId")]
        public Ilce Ilce { get; set; }

        public List<OyParti> OyPartiler { get; set; }

        public List<OyAday> OyAdaylar { get; set; }


    }
}
