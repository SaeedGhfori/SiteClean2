using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Site.Application.Definitions.Contracts.Services.Identity;
using Site.Application.Definitions.Dtos.Identity;
using Site.Application.Implementations.Services.Identity;
using Site.Persistence.Contexts.IdentityContext;
using System.Text;

namespace Site.Persistence.Configurations.IdentityConfigurations.IdentityConfigs
{
    public static class IdentityServicesConfiguration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SiteConnection"),
                    b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityError>();

            services.AddTransient<IAuthService, AuthService>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(o =>
             {
                 var key = configuration["JwtSettings:Key"];
                 if (string.IsNullOrEmpty(key))
                 {
                     throw new InvalidOperationException("JWT Key is not found in the configuration.");
                 }

                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     ValidIssuer = configuration["JwtSettings:Issuer"],
                     ValidAudience = configuration["JwtSettings:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                 };
             });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            });

            return services;
        }
    }
}
