
using Microsoft.EntityFrameworkCore;
using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Employee
{
    public sealed record DeleteEmployeeCommand(int Id) : IRequest<Result>
    {

        private sealed class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<DeleteEmployeeCommand, Result>
        {
            public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                var employee = await employeeRepository.QueryAll().FirstOrDefaultAsync(e => e.EmployeeId == request.Id);
                if(employee == null)
                {
                    return Result.Fail($"Employee with Id {request.Id} not found.");
                }
                await employeeRepository.DeleteAsync(employee,cancellationToken);
                return Result.Success();
            }
        }
    }
}
