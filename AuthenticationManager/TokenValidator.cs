using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace AuthenticationManager
{
    public class TokenValidator
    {
        public readonly RequestDelegate _next;
        private const string Token = "Johnson123";

        public TokenValidator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Faltan datos de autenticacion");
                return;
            }

            if (!string.Equals(tokenHeader, $"Bearer {Token}", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token no válido");
                return;
            }

            await _next(context);
        }
    }
}