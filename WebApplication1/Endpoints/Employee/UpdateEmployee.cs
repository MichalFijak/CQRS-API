using Application.Commands.Employee;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class UpdateEmployee
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapPut("/{employeeId:int}", HandleUpdateEmployee);

        }


        public static async Task<IResult> HandleUpdateEmployee(int employeeId,[FromBody] EmployeeDto employee, [FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new UpdateEmployeeCommand(employeeId, employee));

            return result.IsSuccess
                ? TypedResults.Ok(new EmployeeDto())
                : TypedResults.NotFound(result.Error);
        }
    }
}
