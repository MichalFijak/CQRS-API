
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.UserQueries
{
    public sealed record GetUserByIdQuerry(int Id) : IRequest<UserDto>
    {

    }

    public sealed class GetUserByIdQuerryHandler : IRequestHandler<GetUserByIdQuerry, UserDto>
    {

        private readonly IUserRepository userRepository;
        public GetUserByIdQuerryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuerry request, CancellationToken cancellationToken)
        {
            var user =await userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new Exception($"User with id {request.Id} not found");
            }
            var userDto = UserMapper.ToUserDto(user);
            return userDto;
        }
    }


}
