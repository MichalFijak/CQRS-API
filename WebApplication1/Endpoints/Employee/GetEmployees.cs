using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public static class GetEmployees
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("", HandleGetEmployees);

        }

        public static async Task<IResult> HandleGetEmployees([FromServices] IMediator mediator)
        {
            //var employess = mediator.Send(GetEmployeesQuerry);
            return Results.NotFound();
        }
    }
}
