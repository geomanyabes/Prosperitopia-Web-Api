using Prosperitopia.Application.Interface.Service;
using Prosperitopia.Application.Interface.Validator;
using Prosperitopia.Application.Service;
using Prosperitopia.Application.Validator;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.DataAccess.Repository;

namespace Prosperitopia.Web.Api.Configuration
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IItemValidator, ItemValidator>();
            services.AddScoped<ICategoryValidator, CategoryValidator>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
