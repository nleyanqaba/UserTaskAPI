using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTasksAPI.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string PasswordHash { get; set; }

        public string? Token { get; set; }

        [NotMapped]
        public string Password { get; set; }
    }
}
