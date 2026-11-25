using Microsoft.Extensions.DependencyInjection;
using smartVenue.Application.Interfaces;
namespace smartVenue.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Register Application Services here

        return services;
    }
}