using Api.Mappers;
using Api.Response;
using Application.Common;
using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Employee
{
    public class GetAllEmployees
    {


        public static void Map(IEndpointRouteBuilder group)
        {

            group.MapGet("/all", HandleGetAllEmployee);

        }


        private static async Task<Results<Ok<PagedResponse<EmployeeResponse>>, NotFound>> HandleGetAllEmployee([AsParameters] EmployeeQueryFilter queryFilter, [FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetAllEmployeesQuerry(queryFilter));
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
