using Application.Common;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Commands.Employee
{
    public sealed record RestoreEmployeeCommand(int id) : IRequest<Result<EmployeeDto>>
    {
        private sealed class RestoreEmployeeCommandHandler : IRequestHandler<RestoreEmployeeCommand, Result<EmployeeDto>>
        {
            private readonly IEmployeeRepository employeeRepository;
            public RestoreEmployeeCommandHandler(IEmployeeRepository employeeRepository)
            {
                this.employeeRepository = employeeRepository;
            }
            public async Task<Result<EmployeeDto>> Handle(RestoreEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employee = await employeeRepository.QueryAll().FirstOrDefaultAsync(e=>e.EmployeeId== request.id, cancellationToken);

                if(employee == null)
                {
                    return Result<EmployeeDto>.Fail($"Employee with id {request.id} not found.");
                }
                employee.IsDeleted = false;
                employee.DeletedAt = null;
                employee.DeletedBy = null;

                await employeeRepository.UpdateAsync(employee, cancellationToken);
                var employeeDto = employee.ToEmployeeDto();
                return Result<EmployeeDto>.Success(employeeDto);
            }
        }
    }
}