using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class DeleteEmployee
    {

        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/{id:int}", HandleRemoveEmoployee);

        }



        public static async Task<IResult> HandleRemoveEmoployee(int id, [FromServices] IMediator mediator)
        {
            return Results.NotFound();
        }
    }
}
