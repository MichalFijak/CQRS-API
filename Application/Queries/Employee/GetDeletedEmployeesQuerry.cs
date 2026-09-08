using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;



namespace Application.Queries.Employee
{
    public sealed record GetDeletedEmployeesQuerry() : IRequest<List<EmployeeDto>>
    {


        internal sealed class GetDeletedEmployeesQuerryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetDeletedEmployeesQuerry, List<EmployeeDto>>
        {
            public async Task<List<EmployeeDto>> Handle(GetDeletedEmployeesQuerry request, CancellationToken cancellationToken)
            {

                var items = employeeRepository.GetDeletedEmployeeQuery();

                var dtoItems = items.Select(e => e.ToEmployeeDto()).ToList();

                return dtoItems;
            }
        }
    }
}
