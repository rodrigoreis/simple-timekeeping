using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timekeeping.Repositories;
using Timekeeping.Services;

namespace Timekeeping.DependencyInjection
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddTimekeeping(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTimekeepingContext(configuration);
            services.AddRepositories();
            services.AddServices();

            return services;
        }
    }
}
