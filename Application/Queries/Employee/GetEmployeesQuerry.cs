using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Employee
{
    public sealed record GetEmployeesQuerry : IRequest<List<EmployeeDto>>
    {

        internal sealed class GetEmployeeQuerryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeesQuerry, List<EmployeeDto>>
        {
            public async Task<List<EmployeeDto>> Handle(GetEmployeesQuerry request, CancellationToken cancellationToken)
            {
                var employees = await employeeRepository.GetAllAsync(cancellationToken);
                if(employees == null)
                {
                    throw new Exception($"Employees not found");
                }
                var employeesDto = employees.Select(e => e.ToEmployeeDto()).ToList();
                return employeesDto;
            }
        }
    }
}
