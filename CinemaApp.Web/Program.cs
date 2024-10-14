using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("SQLServer");

            // Add services to the container.
            builder.Services.AddDbContext<CinemaDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
            {
                ConfigureIdentity(cfg, builder);
            })
            .AddEntityFrameworkStores<CinemaDbContext>()
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddUserManager<UserManager<ApplicationUser>>();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Identity/Account/Login";
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.ApplyMigrations();
            app.Run();
        }

        private static void ConfigureIdentity(IdentityOptions cfg, WebApplicationBuilder builder)
        {
            cfg.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowerCase");
            cfg.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphaNumerical");
            cfg.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUpperCase");
            cfg.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequireLength");
            cfg.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequireUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            cfg.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}
