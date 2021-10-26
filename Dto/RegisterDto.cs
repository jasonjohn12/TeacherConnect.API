using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TeacherConnect.Entities;

namespace TeacherConnect.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 4)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}