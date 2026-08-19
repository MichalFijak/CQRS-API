using Application.Common;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Application.Queries.Employee
{
    public sealed record GetAllEmployeesQuerry(EmployeeQueryFilter queryFilter) : IRequest<PagedResponse<EmployeeDto>>
    {

        private sealed class GetAllEmployeesQuerryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetAllEmployeesQuerry, PagedResponse<EmployeeDto>>
        {
            public async Task<PagedResponse<EmployeeDto>> Handle(GetAllEmployeesQuerry request, CancellationToken cancellationToken)
            {

                var filters = request.queryFilter;

                var query = employeeRepository.GetAllAsync();

                if (!string.IsNullOrEmpty(filters.Search))
                {
                    query = query.Where(e => e.Username.Contains(filters.Search));
                }

                query = filters.SortBy switch
                {
                    "username" => query.OrderBy(e => e.Username),
                    "salary" => query.OrderBy(e => e.Salary),
                    _ => query
                };

                var totalCount = await query.CountAsync(cancellationToken);

                var items = await query
                    .Skip((filters.PageNumber - 1) * filters.PageSize)
                    .Take(filters.PageSize)
                    .ToListAsync(cancellationToken);

                var dtoItems = items.Select(e => e.ToEmployeeDto()).ToList();

                return new PagedResponse<EmployeeDto>
                {
                    Data = dtoItems,
                    PageNumber = filters.PageNumber,
                    PageSize = filters.PageSize,
                    TotalRecords = totalCount
                };
            }
        }

    }
}
