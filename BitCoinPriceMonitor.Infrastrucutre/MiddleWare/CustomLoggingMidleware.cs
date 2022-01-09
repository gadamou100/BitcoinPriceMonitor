using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BitCoinPriceMonitor.Infrastructure.MiddleWare
{
    public class CustomLoggingMidleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMidleware> _logger;
        public CustomLoggingMidleware(RequestDelegate next, ILogger<CustomLoggingMidleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation($"Request started at {DateTime.UtcNow}");
                await _next(httpContext);
                _logger.LogInformation($"Request completed at {DateTime.UtcNow}");

            }
            catch (Exception e)
            {
                _logger.LogInformation($"Request failled at {DateTime.UtcNow}");
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
    public static class CustomLoggingMidlewareExtensions
    {
        public static IApplicationBuilder UseCustomLoggingMidleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomLoggingMidleware>();
        }
    }
}
