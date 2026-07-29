

using Application.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Auth
{
public static class Register
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/auth/register", RegisterUser);
    }

    private static async Task<IResult> RegisterUser(
        RegisterUserCommand cmd,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(cmd);

        return result.IsSuccess
            ? Results.Ok()
            : Results.BadRequest(result.Error);
    }
}

}
