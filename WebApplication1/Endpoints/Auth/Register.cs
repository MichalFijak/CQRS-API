

using Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints.Auth
{
public static class Register
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/auth/register", RegisterUser);
    }

        private static async Task<Results<Ok, BadRequest<string>>> RegisterUser(
            RegisterUserCommand cmd,
            IMediator mediator)
        {
            var result = await mediator.Send(cmd);

            return result.IsSuccess
                ? TypedResults.Ok()
                : TypedResults.BadRequest(result.Error);
        }
    }

}
