using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Secim2028.Models
{
    public class Il
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IlId { get; set; }
        public string IlAdi { get; set; }
        public List<Ilce> Ilceler { get; set; }

    }
}
