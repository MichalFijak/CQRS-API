
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EmployeeInfo:ISoftDelete
    {
        [Key]
        public int EmployeeId { get; init; }

        public string Collegue { get; init; } = string.Empty;

        public string Department { get; init; } = string.Empty;

        public string Position { get; init; } = string.Empty;

        public bool IsPromoted { get; init; } = false;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; } = null;

        public Employee? Employee { get; set; }
        public string? DeletedBy { get; set; }
    }
}


