using Prosperitopia.DataAccess.Interface;
using Prosperitopia.DataAccess.Repository;

namespace Prosperitopia.Web.Api.Configuration
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
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
