using Api.Mappers;
using Api.Response;
using Application.Common;
using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public static class GetDeletedEmployee
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/deleted", HandleGetDeletedEmployee);

        }

        public static async Task<Results<Ok<List<EmployeeResponse>>, NotFound>> HandleGetDeletedEmployee([FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetDeletedEmployeesQuerry());

            if (result==null)
                return TypedResults.NotFound();

            var mapped = result.Select(e => e.MapToResponse()).ToList();

            return TypedResults.Ok(mapped);
        }
    }
}
