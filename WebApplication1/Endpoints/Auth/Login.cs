using Application.Commands.User;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints.Auth
{
    public class Login
    {

        public static void Map(WebApplication app)
        {
            app.MapPost("/auth/login", LoginUser);
        }


        private static async Task<Results<Ok<LoginResponse>, NotFound<string>>> LoginUser(
            LoginUserCommand cmd,
            IMediator mediator)
        {
            var result = await mediator.Send(cmd);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }
    }
}
