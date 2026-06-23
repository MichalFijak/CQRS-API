using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pages.UserModel
{
    public class UserWithInfoModel : PageModel
    {
        private readonly HttpClient _http;

        public UserDto? User { get; private set; }
        public UserWithInfoDto? UserInfo { get; private set; }

        public UserWithInfoModel(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("Api");
        }

        public async Task<IActionResult> OnGet(int id)
        {
            // GET /api/users/{id}
            var userResponse = await _http.GetAsync($"/api/users/{id}");
            if (userResponse.IsSuccessStatusCode)
                User = await userResponse.Content.ReadFromJsonAsync<UserDto>();

            // GET /api/users/{id}/info
            var infoResponse = await _http.GetAsync($"/api/users/{id}/info");
            if (infoResponse.IsSuccessStatusCode)
                UserInfo = await infoResponse.Content.ReadFromJsonAsync<UserWithInfoDto>();

            return Page();
        }
    }
}
