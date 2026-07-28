using Application.Dtos;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EmployeeQueries
{
    public sealed record GetEmployeeWithInfoQuerry(int Id) : IRequest<EmployeeInfoDto>
    {
    }

    internal sealed class GetEmployeeWithInfoQuerryHandler : IRequestHandler<GetEmployeeWithInfoQuerry, EmployeeInfoDto>
    {
        private readonly IEmployeeInfoRepository userInfoRepository;
        private readonly IEmployeeRepository userRepository;

        public GetEmployeeWithInfoQuerryHandler(IEmployeeInfoRepository userInfoRepository, IEmployeeRepository userRepository)
        {
            this.userInfoRepository = userInfoRepository;
            this.userRepository = userRepository;
        }
        public async Task<EmployeeInfoDto> Handle(GetEmployeeWithInfoQuerry request, CancellationToken cancellationToken)
        {
            var query =
                from u in userRepository.AsQueryable()
                join ui in userInfoRepository.AsQueryable()
                    on u.EmployeeId equals ui.EmployeeId
                where u.EmployeeId == request.Id
                select new EmployeeInfoDto
                {
                    EmployeeId = u.EmployeeId,
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
