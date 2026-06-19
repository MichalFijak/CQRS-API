
namespace Application.Dtos
{
    public sealed record UserDto
    {
        public int UserId { get; init; }

        public string Username { get; init; } = string.Empty;

        public int Salary { get; init; }

        public string Email { get; init; } = string.Empty;


    }
}
