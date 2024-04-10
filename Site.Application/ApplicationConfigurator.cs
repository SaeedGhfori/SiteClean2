using Microsoft.Extensions.DependencyInjection;
using Site.Application.Definitions.Contracts.Services.Categorys;
using Site.Application.Definitions.Contracts.Services.Invoices;
using Site.Application.Implementations.Services.Categorys;
using Site.Application.Implementations.Services.Invoices;
using System.Reflection;

namespace Site.Application
{
    public static class ApplicationConfigurator
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<ICategoryBrandService, CategoryBrandService>();
            services.AddTransient<ICategoryService, CategoryService>();
        }
    }
}

