using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public static class GetEmployees
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/{id:int}", HandleGetEmployees);

        }

        public static async Task<IResult> HandleGetEmployees([FromServices] IMediator mediator)
        {

            return Results.NotFound();
        }
    }
}
