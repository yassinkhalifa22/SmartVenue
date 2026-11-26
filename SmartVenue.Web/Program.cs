using Microsoft.EntityFrameworkCore;
using SmartVenue.Business.Interfaces;
using SmartVenue.Business.Services.Ticketing;
using SmartVenue.DataAccess.Context;
using SmartVenue.Services;

namespace SmartVenue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register DbContext
            builder.Services.AddDbContext<SmartVenueDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAppDbContext>(provider =>
                provider.GetRequiredService<SmartVenueDbContext>());

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddScoped<IQrTicketService, QrTicketService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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