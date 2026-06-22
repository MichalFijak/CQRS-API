using Application.Dtos;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserQueries
{
    public sealed record GetUserWithInfoQuerry(int Id) : IRequest<UserWithInfoDto>
    {
    }

    public sealed class GetUserWithInfoQuerryHandler : IRequestHandler<GetUserWithInfoQuerry, UserWithInfoDto>
    {
        private readonly IUserInfoRepository userInfoRepository;
        private readonly IUserRepository userRepository;

        public GetUserWithInfoQuerryHandler(IUserInfoRepository userInfoRepository, IUserRepository userRepository)
        {
            this.userInfoRepository = userInfoRepository;
            this.userRepository = userRepository;
        }
        public async Task<UserWithInfoDto> Handle(GetUserWithInfoQuerry request, CancellationToken cancellationToken)
        {
            var query =
                from u in userRepository.AsQueryable()
                join ui in userInfoRepository.AsQueryable()
                    on u.UserId equals ui.UserId
                where u.UserId == request.Id
                select new UserWithInfoDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Collegue = ui.Collegue,
                    Department = ui.Department,
                    Position = ui.Position
                };
            
            var userInfo = await query.FirstOrDefaultAsync(cancellationToken);

            if (userInfo is null)
            {
                throw new Exception("User not found");
            }

            return userInfo;
        }
    }
}
