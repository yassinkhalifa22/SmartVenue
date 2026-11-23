using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using smartVenue.Application;
using smartVenue.core.Interfaces;
using smartVenue.DataAccess;
using smartVenue.Infrastructure;
using smartVenue.Infrastructure.Persistence;
using smartVenue.Infrastructure.Persistence.Seed;
using smartVenue.Web.Services;

namespace smartVenue.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();


            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();



            var app = builder.Build();


            DataSeeder.SeedAsync(app.Services).GetAwaiter().GetResult();


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
            app.UseStaticFiles();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
