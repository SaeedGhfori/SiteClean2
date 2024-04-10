using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Persistence.Contexts.ApplicationContext;
using Site.Persistence.Contexts.IdentityContext;
using Site.Persistence.Contexts.MongoContext;
using Site.Persistence.Repositories;

namespace Site.Persistence
{
    public static class PersistenceConfigurator
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SiteConnection"));
            });

            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryBrandRepository, CategoryBrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();


            return services;
        }
    }
}
