using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class CreateEmployee
    {


        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapPost("/{id:int}", HandleAddEmployee);

        }

        public static async Task<IResult> HandleAddEmployee(int id, [FromServices] IMediator mediator)
        {
            return Results.NotFound();
        }
    }
}
