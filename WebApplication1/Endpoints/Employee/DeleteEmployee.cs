using Application.Commands.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class DeleteEmployee
    {

        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapDelete("/{id:int}", HandleRemoveEmoployee);

        }



        public static async Task<IResult> HandleRemoveEmoployee(int id, [FromServices] IMediator mediator)
        {
            var result =await mediator.Send(new DeleteEmployeeCommand(id));
            return result.IsSuccess ? Results.Ok() : Results.NotFound(result.Error);
        }
    }
}
