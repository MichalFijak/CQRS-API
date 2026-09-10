using Application.Dtos;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Employee
{
    public sealed record GetEmployeeWithInfoQuerry(int Id) : IRequest<EmployeeInfoDto>
    {
    }

    internal sealed class GetEmployeeWithInfoQuerryHandler : IRequestHandler<GetEmployeeWithInfoQuerry, EmployeeInfoDto>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeeWithInfoQuerryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<EmployeeInfoDto> Handle(GetEmployeeWithInfoQuerry request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.AsQueryable()
                .Include(e => e.EmployeeInfo)
                .FirstOrDefaultAsync(e => e.EmployeeId == request.Id, cancellationToken);

            if (employee?.EmployeeInfo is null)
                throw new Exception("User not found");

            return new EmployeeInfoDto
            {
                EmployeeId = employee.EmployeeId,
                Username = employee.Username,
                Collegue = employee.EmployeeInfo.Collegue,
                Department = employee.EmployeeInfo.Department,
                Position = employee.EmployeeInfo.Position
            };
        }
    }
}
