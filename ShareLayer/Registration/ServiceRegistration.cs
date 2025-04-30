using Application.Interfaces;
using Domain.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareLayer.Service;


namespace ShareLayer.Registration
{
    public static class ServiceRegistration
    {
        public static void AddShareLayerRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();

        }

    }
}
