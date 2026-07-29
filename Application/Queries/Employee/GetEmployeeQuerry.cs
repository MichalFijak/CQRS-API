using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Employee
{
    public sealed record GetEmployeeQuerry(int Id) : IRequest<EmployeeDto>
    {

    }

    internal sealed class GetEmployeeQuerryHandler : IRequestHandler<GetEmployeeQuerry, EmployeeDto>
    {

        private readonly IEmployeeRepository employeeRepository;
        public GetEmployeeQuerryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuerry request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (employee == null)
            {
                throw new Exception($"Employee with id {request.Id} not found");
            }
            var employeeDto = EmployeeMapper.ToEmployeeDto(employee);
            return employeeDto;
        }
    }


}
