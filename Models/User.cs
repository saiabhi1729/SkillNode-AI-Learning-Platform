using System.ComponentModel.DataAnnotations;

namespace SkillNode_Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "Student";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
