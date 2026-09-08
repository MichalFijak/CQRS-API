using Api.Response;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Api.Pages.Manager
{
    public class DashboardModel : PageModel
    {
        private readonly HttpClient _http;

        public DashboardModel(IHttpClientFactory httpClientFactory)
            => _http = httpClientFactory.CreateClient("Api");

        public List<EmployeeResponse>? ActiveEmployees { get; private set; }
        public List<EmployeeResponse>? DeletedEmployees { get; private set; }
        public EmployeeResponse? RestoredEmployee { get; private set; }
        public int Salary { get; private set; }=0;
        public string Email { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string Error { get; private set; } = string.Empty;
        public void OnGet() { }

        public async Task<IActionResult> OnGetAll()
        {

            var employeeResponse = await _http.GetAsync("/api/employees/all");
            if (employeeResponse.IsSuccessStatusCode)
            {
                ActiveEmployees = await employeeResponse.Content.ReadFromJsonAsync<List<EmployeeResponse>>();
            }
            else
            {
                Error = employeeResponse.StatusCode.ToString();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRestore(int employeeId)
        {


            var employeeResponse = await _http.PostAsync($"/api/employees/restore/{employeeId}", new StringContent(""));
            if (employeeResponse.IsSuccessStatusCode)
            {
                RestoredEmployee = await employeeResponse.Content.ReadFromJsonAsync<EmployeeResponse>();
            }
            else
            {
                Error = employeeResponse.StatusCode.ToString();
            }
            return Page();
        }

        public async Task<IActionResult> OnGetDeleted()
        {

            var employeeResponse = await _http.GetAsync("api/employees/deleted");
            if (employeeResponse.IsSuccessStatusCode)
            {
                DeletedEmployees = await employeeResponse.Content.ReadFromJsonAsync<List<EmployeeResponse>>();
            }
            else
            {
                Error = employeeResponse.StatusCode.ToString();
            }
            return Page();

        }
        public async Task<IActionResult> OnPostUpdate(int employeeId, string Username, string Email, int Salary)
        {
            var dto = new EmployeeDto
            {
                Username = Username,
                Email = Email,
                Salary = Salary
            };

            var json = JsonContent.Create(dto);

            var response = await _http.PutAsync(
                $"/api/employees/{employeeId}",
                json
            );

            if (response.IsSuccessStatusCode)
            {
                RestoredEmployee = await response.Content.ReadFromJsonAsync<EmployeeResponse>();
            }
            else
            {
                Error = response.StatusCode.ToString();
            }

            return Page();
        }
    }
}