namespace Api.Providers
{
    public class AuthTokenHandler(IHttpContextAccessor httpContextAccessor) :DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var token = httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
            if(!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
            }


            return await base.SendAsync(request,cancellationToken);
        }

    }
}
