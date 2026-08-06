using Api.Mappers;
using Api.Response;
using Application.Common;
using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public static class GetEmployees
    {
        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/", HandleGetEmployees);

        }

        public static async Task<Results<Ok<PagedResponse<EmployeeResponse>>, NotFound>> HandleGetEmployees([AsParameters] EmployeeQueryFilter queryFilter,[FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetEmployeesQuerry(queryFilter));

            if (!result.Data.Any())
                return TypedResults.NotFound();

            var mapped = result.Data
                .Select(e => e.MapToResponse())
                .ToList();

            return TypedResults.Ok(new PagedResponse<EmployeeResponse>
            {
                Data = mapped,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            });
        }
    }
}
