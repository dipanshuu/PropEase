using System.ComponentModel.DataAnnotations;
namespace PropEase.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }

    }
}
