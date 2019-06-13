using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Timekeeping.Services.Abstractions;

namespace Timekeeping.Services
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();
            return services;
        }
    }
}
