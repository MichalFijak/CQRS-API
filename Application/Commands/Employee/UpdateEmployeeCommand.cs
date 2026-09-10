using Application.Common;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Employee
{
    public sealed record UpdateEmployeeCommand(int id, EmployeeDto employeeNewData) : IRequest<Result<EmployeeDto>>
    {

        private sealed class UpdateEmployeeCommandQuery(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
        {
            public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employee =await employeeRepository.GetByIdAsync(request.id, cancellationToken);

                if (employee == null)
                {
                    return Result<EmployeeDto>.Fail($"Employee with id {request.id} not found.");
                }
                
                employee.Salary = request.employeeNewData.Salary;
                employee.Email = request.employeeNewData.Email;
                employee.Username = request.employeeNewData.Username;

                await employeeRepository.UpdateAsync(employee, cancellationToken);
                var employeeDto = employee.ToEmployeeDto();
                return Result<EmployeeDto>.Success(employeeDto);
            }
        }

    }
}
