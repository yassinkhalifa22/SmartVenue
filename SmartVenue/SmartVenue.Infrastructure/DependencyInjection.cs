using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartVenue.Application.Common;
using SmartVenue.Application.Common.Interfaces;
using SmartVenue.Domain.Entities.Identity;
using SmartVenue.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
namespace SmartVenue.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
