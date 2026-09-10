using Api.Response;
using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.Employees
{
    public class EmployeesModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly HttpClient _http = httpClientFactory.CreateClient("Api");

        public List<EmployeeResponse>? Employees { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRecords { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public string? Error { get; private set; }

        public async Task<IActionResult> OnGet(int pageNumber = 1, int pageSize = 10)
        {
            var employeeResponse = await _http.GetAsync($"/api/employees?pageNumber={pageNumber}&pageSize={pageSize}");

            if (!employeeResponse.IsSuccessStatusCode)
            {
                Error = employeeResponse.StatusCode.ToString();
                return Page();
            }

            var paged = await employeeResponse.Content.ReadFromJsonAsync<PagedResponse<EmployeeResponse>>();

            if (paged is null)
            {
                Error = "Invalid response";
                return Page();
            }

            Employees = paged.Data.ToList();
            PageNumber = paged.PageNumber;
            PageSize = paged.PageSize;
            TotalRecords = paged.TotalRecords;
            TotalPages = paged.TotalPages;
            HasNextPage = paged.HasNextPage;
            HasPreviousPage = paged.HasPreviousPage;

            return Page();
        }

        public async Task<IActionResult> OnPostRemove(int id)
        {
            var response = await _http.DeleteAsync($"/api/employees/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Error = response.StatusCode.ToString();
                return Page();
            }
            return RedirectToPage("/Employees/Employees");
        }
    }
}
