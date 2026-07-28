
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Salary { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
