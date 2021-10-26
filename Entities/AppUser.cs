using System;
using System.Text.Json.Serialization;

namespace TeacherConnect.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public DateTime LastLoggedInDate { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Email { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role UserRole { get; set; }
        public Boolean IsActive { get; set; }

    }
}