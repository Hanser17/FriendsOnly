using Application.Interfaces;
using Identity.Context;
using Identity.Entities;
using Identity.Seeds;
using Identity.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Identity.Registration
{
    public static class IdentityServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
           
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            
            #endregion

            #region Identity

            services.AddIdentityCore<User>()
                    .AddRoles<IdentityRole>()
                    .AddSignInManager()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromSeconds(300);
            });

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;

            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromHours(24);
                opt.LoginPath = "/User";
                opt.AccessDeniedPath = "/User/AccessDenied";
            });
            #endregion

            #region IdentityService
            services.AddTransient<IIdentityService, IdentityService>();
            #endregion




        }
        public static async Task RunSeeds( this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var servicesprovider = scope.ServiceProvider;
                try
                {
                    var userManager = servicesprovider.GetRequiredService<UserManager<User>>();
                    var roleManager = servicesprovider.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultBasicUser.SeedAsync(userManager);
                    await DefaultSuperAdminUser.SeedAsync(userManager);

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
