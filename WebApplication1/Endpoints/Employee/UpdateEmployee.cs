using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class UpdateEmployee
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/{id:int}", HandleUpdateEmployee);

        }


        public static async Task<IResult> HandleUpdateEmployee(int id, [FromServices] IMediator mediator)
        {
            return Results.NotFound();
        }
    }
}
