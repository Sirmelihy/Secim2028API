using System.ComponentModel.DataAnnotations;

namespace Secim2028.Models
{
    public class User
    {
        [Key]
        public int uID { get; set; }
        public string username { get; set; } = string.Empty;
        public string hashPassword { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
}
