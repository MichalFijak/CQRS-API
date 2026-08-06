using Application.Common;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Employee
{
    public sealed record GetEmployeesQuerry(EmployeeQueryFilter queryFilter) : IRequest<PagedResponse<EmployeeDto>>
    {

        internal sealed class GetEmployeeQuerryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeesQuerry, PagedResponse<EmployeeDto>>
        {
            public async Task<PagedResponse<EmployeeDto>> Handle(GetEmployeesQuerry request, CancellationToken cancellationToken)
            {
                var filters = request.queryFilter;

                var query = employeeRepository.GetEmployeeQuery();

                // 🔍 Search
                if (!string.IsNullOrEmpty(filters.Search))
                {
                    query = query.Where(e => e.Username.Contains(filters.Search));
                }

                // 🔽 Sort
                query = filters.SortBy?.ToLower() switch
                {
                    "username" => query.OrderBy(e => e.Username),
                    "salary" => query.OrderBy(e => e.Salary),
                    _ => query.OrderBy(e => e.EmployeeId)
                };

                var totalCount = await query.CountAsync(cancellationToken);

                // 📄 Pagination (SQL)
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

