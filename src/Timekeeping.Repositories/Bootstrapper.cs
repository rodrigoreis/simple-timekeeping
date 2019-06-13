using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Timekeeping.Repositories.Abstractions;
using Timekeeping.Repositories.Abstractions.Models;
using Timekeeping.Repositories.Contexts;

namespace Timekeeping.Repositories
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddTimekeepingContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TimekeepingContext>(
                options => options.UseSqlite(configuration["ConnectionStrings:Default"])
            );

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IRepository<UserModel>, UserRepository>();

            return services;
        }
    }
}
