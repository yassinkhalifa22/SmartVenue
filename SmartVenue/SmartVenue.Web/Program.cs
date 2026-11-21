using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartVenue.Application.Common;
using SmartVenue.Application.Common.Interfaces;
using SmartVenue.Domain.Entities.Identity;
using SmartVenue.Infrastructure.Persistence;
using SmartVenue.Web.Services;

namespace SmartVenue.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();


            builder.Services.AddDbContext<SmartVenueDbContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




            builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<SmartVenueDbContext>());

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();



            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<SmartVenueDbContext>()
                .AddDefaultTokenProviders();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
