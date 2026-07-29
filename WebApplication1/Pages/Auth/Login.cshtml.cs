using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _http;

        public LoginModel(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        [BindProperty] public string Username { get; set; } = string.Empty;
        [BindProperty] public string Password { get; set; } = string.Empty;

        public string? Error { get; private set; }

        public async Task<IActionResult> OnPost()
        {
            var response = await _http.PostAsJsonAsync("/auth/login", new
            {
                username = Username,
                password = Password
            });

            if (!response.IsSuccessStatusCode)
            {
                Error = "Invalid credentials";
                return Page();
            }

            var json = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            var token = json["token"];

            // TODO: store token in cookie/session

            return RedirectToPage("/Index");
        }
    }

}
