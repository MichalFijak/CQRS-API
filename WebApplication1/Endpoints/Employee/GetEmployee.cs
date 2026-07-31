using Api.Response;
using Application.Common;
using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public static class GetEmployee
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/{id:int}", HandleGetEmployee);

        }

        public static async Task<Results<Ok<EmployeeResponse>, NotFound<string>>> HandleGetEmployee(
            int id,
            [FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetEmployeeQuerry(id));

            if (result is null)
                return TypedResults.NotFound("User not found");

            var response = new EmployeeResponse(
                result.EmployeeId,
                result.Username,
                result.Salary,
                result.Email
            );

            return TypedResults.Ok(response);
        }
        public static async Task<IResult> HandleGetEmployeeInfo(int id, [FromServices] IMediator mediator)
        {
            var employee = await mediator.Send(new GetEmployeeWithInfoQuerry(id));

            return employee is null ? Results.NotFound() : Results.Ok(employee);
        }


    }
}
