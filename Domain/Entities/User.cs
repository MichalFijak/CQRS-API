
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Salary { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
