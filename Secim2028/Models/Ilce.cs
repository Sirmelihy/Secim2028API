using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Secim2028.Models
{
    public class Ilce
    {
        [Key]
        public int IlceId { get; set; }
        public string IlceAdi { get; set; }

        [ForeignKey("IlId")]
        public Il Il { get; set; }

        public List<Sandik> Sandiklar { get; set; }



    }
}
