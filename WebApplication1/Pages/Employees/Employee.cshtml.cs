using Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.Employees
{
    public class EmployeeModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly HttpClient _http = httpClientFactory.CreateClient("Api");

        public EmployeeResponse? Employee { get; private set; }
        public string? Error { get; private set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var employeeResponse = await _http.GetAsync($"/api/employees/{id}");
            if (employeeResponse.IsSuccessStatusCode)
                Employee = await employeeResponse.Content.ReadFromJsonAsync<EmployeeResponse>();
            else
            {
                Error = employeeResponse.StatusCode.ToString();
            }
            return Page();
        }
    }
}
