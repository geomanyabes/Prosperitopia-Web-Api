using Prosperitopia.Application.Interface.Service;
using Prosperitopia.Application.Interface.Service.Authentication;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.Application.Service;
using Prosperitopia.Application.Service.Authentication;
using Prosperitopia.Application.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.DataAccess.Repository;

namespace Prosperitopia.Web.Api.Configuration
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Authentication
            services.AddSingleton<IAuthService, ApiKeyAuthService>();

            #endregion

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IItemValidator, ItemValidator>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            return services;
        }
    }
}
