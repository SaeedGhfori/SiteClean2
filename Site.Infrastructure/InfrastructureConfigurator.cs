using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Application.Definitions.Contracts.Interfaces.Emails;
using Site.Application.Definitions.Contracts.Interfaces.Products;
using Site.Application.Definitions.Models.Products;
using Site.Application.Helpers.RestSharp;
using Site.Infrastructure.Helpers.RestSharp;
using Site.Infrastructure.Mail;
using Site.Infrastructure.Services.Products;

namespace Site.Infrastructure
{
    public static class InfrastructureConfigurator
    {
        public static IServiceCollection ConfigureInfrastractureServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            // Configure other services
            services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();


            services.AddTransient<IProductService, ProductService>();


            #region Middleware Url

            string productBaseUrl = configuration.GetSection("RestClientSettings:ProductBaseUrl").Value;
            services.AddSingleton<IRestClient<Product>>(new RestRequestHelper<Product>(productBaseUrl));

            #endregion

            return services;
        }
    }
}
