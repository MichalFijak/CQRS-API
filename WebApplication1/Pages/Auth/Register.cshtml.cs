using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _http;

        public RegisterModel(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        [BindProperty] public string Username { get; set; } = string.Empty;
        [BindProperty] public string Password { get; set; } = string.Empty;

        public string? Error { get; private set; }


        public async Task<IActionResult> OnPost()
        {
            var response = await _http.PostAsJsonAsync("/auth/register", new
            {
                username = Username,
                password = Password
            });

            if (!response.IsSuccessStatusCode)
            {
                Error = await response.Content.ReadAsStringAsync();
                return Page();
            }

            return RedirectToPage("/auth/Login");
        }
    }
}
