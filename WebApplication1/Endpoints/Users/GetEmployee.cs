using Application.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Users;

public static class GetEmployee
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/users/{id}", HandleGetEmployee);
        app.MapGet("/api/users/{id}/info", HandleGetEmployeeInfo);

    }

    public static async Task<IResult> HandleGetEmployee(int id,[FromServices] IMediator mediator)
    {
        var employee = await mediator.Send(new GetEmployeeQuerry(id));
        return employee is null ? Results.NotFound() : Results.Ok(employee);
    }
    public static async Task<IResult> HandleGetEmployeeInfo(int id, [FromServices] IMediator mediator)
    {
        var employee = await mediator.Send(new GetEmployeeWithInfoQuerry(id));
        return employee is null ? Results.NotFound() : Results.Ok(employee);
    }
}
