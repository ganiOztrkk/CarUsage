using CarUsage.Core.GenericRepository;
using Microsoft.Extensions.DependencyInjection;

namespace CarUsage.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(
        this IServiceCollection services)
    {

        return services;
    }
}