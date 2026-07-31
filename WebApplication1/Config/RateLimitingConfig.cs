using Microsoft.AspNetCore.RateLimiting;

namespace Api.Config
{
    public static class RateLimitingConfig
    {
        public static void AddEmployeeRateLimiting(this RateLimiterOptions options)
        {
            options.AddFixedWindowLimiter("employeesPolicy", limiterOptions =>
            {
                limiterOptions.PermitLimit = 10;
                limiterOptions.Window = TimeSpan.FromSeconds(30);
                limiterOptions.QueueLimit = 0;
            });
        }
    }
}