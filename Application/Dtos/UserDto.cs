
namespace Application.Dtos
{
    public sealed record EmployeeDto
    {
        public int EmployeeId { get; init; }

        public string Username { get; init; } = string.Empty;

        public int Salary { get; init; }

        public string Email { get; init; } = string.Empty;


    }
}
