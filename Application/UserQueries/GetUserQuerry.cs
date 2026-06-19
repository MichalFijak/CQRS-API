
using Application.Dtos;
using MediatR;

namespace Application.UserQueries
{
    public sealed record GetUserByIdQuerry(int Id) : IRequest<UserDto>
    {

    }


    public sealed class GetUserByIdQuerryHandler : IRequestHandler<GetUserByIdQuerry, UserDto>
    {

        public GetUserByIdQuerryHandler()
        {
        }

        public Task<UserDto> Handle(GetUserByIdQuerry request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}
