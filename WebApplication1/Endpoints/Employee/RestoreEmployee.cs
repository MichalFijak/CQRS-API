using Api.Response;
using Application.Commands.Employee;
using Application.Dtos;

using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class RestoreEmployee
    {

        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapPost("restore/{employeeId:int}", HandleRestoreEmployee);

        }

        public static async Task<Results<Ok<EmployeeDto>, NotFound<string>>> HandleRestoreEmployee(
    int employeeId,
    [FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new RestoreEmployeeCommand(employeeId));

            return result.IsSuccess
                ? TypedResults.Ok(new EmployeeDto())
                : TypedResults.NotFound(result.Error);
        }
    }
}
