using CarUsage.Application.Mapping;
using CarUsage.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarUsage.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(
                typeof(DependencyInjection).Assembly,
                typeof(AppUser).Assembly);
        });

        services.AddAutoMapper(typeof(MappingProfile));
        
        
        return services;
    }
}