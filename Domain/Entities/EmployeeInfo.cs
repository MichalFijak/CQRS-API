
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EmployeeInfo
    {
        [Key]
        public int EmployeeId { get; init; }

        public string Collegue { get; init; } = string.Empty;

        public string Department { get; init; } = string.Empty;

        public string Position { get; init; } = string.Empty;

        public bool IsPromoted { get; init; } = false;

        public Employee? Employee { get; set; }

    }
}


