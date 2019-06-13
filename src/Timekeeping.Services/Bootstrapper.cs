using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Timekeeping.Services.Abstractions;
using Timekeeping.Services.Abstractions.Dtos;

namespace Timekeeping.Services
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IDtoService<UserDto>, UserService>();
            return services;
        }
    }
}
