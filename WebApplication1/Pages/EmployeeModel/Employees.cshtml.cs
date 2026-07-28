using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.EmployeeModel
{
    public class EmployeeModel : PageModel
    {
        private readonly HttpClient _http;

        public EmployeeDto? Employee { get; private set; }

        public EmployeeModel(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("Api");
        }

        public async Task<IActionResult> OnGet(int id)
        {
            // GET /api/employees/{id}
            var employeeResponse = await _http.GetAsync($"/api/employees/{id}");
            if (employeeResponse.IsSuccessStatusCode)
                Employee = await employeeResponse.Content.ReadFromJsonAsync<EmployeeDto>();

            return Page();
        }
    }
}
