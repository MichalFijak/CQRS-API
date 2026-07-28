using Application.EmployeeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee;

public static class GetEmployee
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/employees/{id}", HandleGetEmployee);
        app.MapGet("/api/employees/{id}/info", HandleGetEmployeeInfo);

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
