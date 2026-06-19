using Application.UserQueries;
using MediatR;

namespace Api.Endpoints.Users
{
    public static class GetUser
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/users/{id}", HandleGetUser);
            app.MapGet("/api/users/{id}/info", HandleGetUserWithInfo);

        }

        public static async Task<IResult> HandleGetUser(int id, IMediator mediator)
        {
            var user = await mediator.Send(new GetUserByIdQuerry(id));
            return user is null ? Results.NotFound() : Results.Ok(user);
        }
        public static async Task<IResult> HandleGetUserWithInfo(int id, IMediator mediator)
        {
            var user = await mediator.Send(new GetUserWithInfoQuerry(id));
            return user is null ? Results.NotFound() : Results.Ok(user);
        }
    }
}
