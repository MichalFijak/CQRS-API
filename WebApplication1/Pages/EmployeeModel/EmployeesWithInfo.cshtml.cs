using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.EmployeeModel
{
    public class EmployeeInfoModel : PageModel
    {
        private readonly HttpClient _http;

        public EmployeeDto? Employee { get; private set; }
        public EmployeeInfoDto? EmployeeInfo { get; private set; }

        public EmployeeInfoModel(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("Api");
        }

        public async Task<IActionResult> OnGet(int id)
        {
            // GET /api/employees/{id}
            var employeeResponse = await _http.GetAsync($"/api/employees/{id}");
            if (employeeResponse.IsSuccessStatusCode)
                Employee = await employeeResponse.Content.ReadFromJsonAsync<EmployeeDto>();

            // GET /api/employees/{id}/info
            var employeeInfoResponse = await _http.GetAsync($"/api/employees/{id}/info");
            if (employeeInfoResponse.IsSuccessStatusCode)
                EmployeeInfo = await employeeInfoResponse.Content.ReadFromJsonAsync<EmployeeInfoDto>();

            return Page();
        }
    }
}
