using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Secim2028.Models
{
    public class AdayCumhurbaskani
    {

        [Key]
        public int AdayId { get; set; }

        public string AdayAdi { get; set; }

        [ForeignKey("SiyasiPartiId")]
        public SiyasiParti SiyasiParti { get; set; }

        public List<OyAday> OyAdays { get; set; }




    }
}
