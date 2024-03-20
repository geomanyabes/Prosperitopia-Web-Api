using Microsoft.Extensions.Configuration;
using Prosperitopia.Application.Interface.Service.Authentication;

namespace Prosperitopia.Application.Service.Authentication
{
    public class ApiKeyAuthService : IAuthService
    {
        private readonly string[] apiKeys;

        public ApiKeyAuthService(IConfiguration configuration) 
        {
            apiKeys = configuration.GetSection("RegisteredApiKeys").Get<string[]>();
        }

        public bool ValidateToken(string token)
        {
            return apiKeys.Contains(token);
        }
    }
}
