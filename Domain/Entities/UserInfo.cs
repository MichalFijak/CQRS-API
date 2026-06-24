
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; init; }

        public string Collegue { get; init; } = string.Empty;

        public string Department { get; init; } = string.Empty;

        public string Position { get; init; } = string.Empty;

        public bool IsPromoted { get; init; } = false;

    }
}


