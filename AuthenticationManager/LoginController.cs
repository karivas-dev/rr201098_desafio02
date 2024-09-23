using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuthenticationManager
{
    public class LoginController
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoginController> _logger;

        public LoginController(RequestDelegate next, ILogger<LoginController> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody = await FormatRequest(context.Request);
            _logger.LogInformation("Request: {method} {url} {body}", context.Request.Method, context.Request.Path, requestBody);

            await _next(context);

            _logger.LogInformation("Response: {statusCode}", context.Response.StatusCode);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }
}