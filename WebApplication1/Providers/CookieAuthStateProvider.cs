using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Api.Providers
{
    public class CookieAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor contextAccessor;

        public CookieAuthStateProvider(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = contextAccessor.HttpContext.Request.Cookies["accessToken"];


            if(string.IsNullOrEmpty(token))
            {
                return Task.FromResult(new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity())));
            }

            var identity = new ClaimsIdentity(ParseClaims(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));

        }


        private IEnumerable<Claim> ParseClaims(string jwt)
        {
            var payload = jwt.Split('.')[1];

            var jsonBytes = Convert.FromBase64String(Pad(payload));

            var claimsDict = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return claimsDict.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private string Pad(string base64)
        {
            return base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        }

    }
}
