using Microsoft.AspNetCore.Http;
using Prosperitopia.Application.Interface.Service.Authentication;


namespace Prosperitopia.Application.Middleware
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private IAuthService _authenticationService;

        public ApiKeyAuthMiddleware(RequestDelegate next, IAuthService authenticationService)
        {
            _next = next;
            _authenticationService = authenticationService;
        }

        public async Task Invoke(HttpContext context)
        {
            var apiKey = context.Request.Headers["X-API-KEY"].FirstOrDefault();

            if (!string.IsNullOrEmpty(apiKey) && _authenticationService.ValidateToken(apiKey))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API Key");
            }
        }
    }
}
