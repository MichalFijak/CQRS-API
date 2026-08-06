
namespace Application.Common
{
    public class EmployeeQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string? Search { get; set; }
    }
}
