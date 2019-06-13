using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;

namespace Timekeeping.Services.Acl
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddAntiCorruptionLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(
                    config => config.AddExpressionMapping(),
                    typeof(Bootstrapper).Assembly
                );

            return services;
        }
    }
}
