
using Application.Dtos;
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

        public Task<UserDto> Handle(GetUserByIdQuerry request, CancellationToken cancellationToken)
        {
            userRepository.GetByIdAsync(request.Id, cancellationToken);
            //mapper
            throw new NotImplementedException();
        }
    }


}
