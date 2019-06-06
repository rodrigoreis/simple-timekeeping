using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timekeeping.Services.Contexts;

namespace Timekeeping.Services
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TimekeepingContext>(options => options.UseSqlite(configuration["ConnectionStrings:Default"]));

            return services;
        }
    }
}
