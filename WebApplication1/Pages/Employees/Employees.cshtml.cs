using Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.Employees
{
    public class EmployeesModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly HttpClient _http = httpClientFactory.CreateClient("Api");

        public List<EmployeeResponse>? Employees { get; private set; }
        public string? Error { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            var employeeResponse = await _http.GetAsync($"/api/employees");
            if (employeeResponse.IsSuccessStatusCode)
                Employees = await employeeResponse.Content.ReadFromJsonAsync<List<EmployeeResponse>>();
            else
            {
                Error = employeeResponse.StatusCode.ToString();
            }
            return Page();
        }
    }
}
