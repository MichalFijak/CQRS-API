
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Employee :ISoftDelete
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Salary { get; set; }

        public string Email { get; set; } = string.Empty;

        public EmployeeInfo? EmployeeInfo { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
