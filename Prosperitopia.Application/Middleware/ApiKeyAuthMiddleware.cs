using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
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
