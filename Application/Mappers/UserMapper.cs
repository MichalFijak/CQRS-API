
using Application.Dtos;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this Domain.Entities.User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Salary = user.Salary,
                Username = user.Username
            };
        }

    }
}
