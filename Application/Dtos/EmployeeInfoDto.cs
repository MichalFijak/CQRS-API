
namespace Application.Dtos
{
    public sealed record EmployeeInfoDto
    {
        public int EmployeeId { get; init; }

        public string Username { get; init; } = string.Empty;

        public int Salary { get; init; }

        public string Email { get; init; } = string.Empty;

        public string Collegue { get; init; } = string.Empty;

        public string Department { get; init; } = string.Empty;

        public string Position { get; init; } = string.Empty;

    }
}
