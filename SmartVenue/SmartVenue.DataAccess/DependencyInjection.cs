
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using smartVenue.core.Entities.Identity;
using smartVenue.Application.Interfaces;
using smartVenue.DataAccess.Context;

namespace smartVenue.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SmartVenueDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppDbContext>(provider =>
                provider.GetRequiredService<SmartVenueDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<SmartVenueDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
