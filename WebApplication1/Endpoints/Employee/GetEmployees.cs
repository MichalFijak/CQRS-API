using Api.Mappers;
using Api.Response;
using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public static class GetEmployees
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("", HandleGetEmployees);

        }

        public static async Task<Results<Ok<List<EmployeeResponse>>, NotFound>> HandleGetEmployees([FromServices] IMediator mediator)
        {
            var employess =await mediator.Send(new GetEmployeesQuerry());
            if (!employess.Any())
                return TypedResults.NotFound();

            var response = employess.Select(e => e.MapToResponse()).ToList();

            return TypedResults.Ok(response);
        }
    }
}
