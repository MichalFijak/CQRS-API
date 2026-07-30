namespace Api.State
{
    public class AuthStateService(IHttpContextAccessor httpContextAccessor) : IAuthStateService
    {
        public bool IsLogged()
        {
            var token = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];

            return !string.IsNullOrEmpty(token);
        }
    }
}
