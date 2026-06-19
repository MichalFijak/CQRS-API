using Application.Dtos;
using MediatR;

namespace Application.UserQueries
{
    public sealed record GetUserWithInfoQuerry(int Id) : IRequest<UserWithInfoDto>
    {
    }

    public sealed class GetUserWithInfoQuerryHandler : IRequestHandler<GetUserWithInfoQuerry, UserWithInfoDto>
    {
        public GetUserWithInfoQuerryHandler()
        {
        }
        public Task<UserWithInfoDto> Handle(GetUserWithInfoQuerry request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
