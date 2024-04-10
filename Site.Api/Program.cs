using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Site.Api.Utilities.Transformers;
using Site.Application;
using Site.Infrastructure;
using Site.Persistence;
using Site.Persistence.Configurations.IdentityConfigurations.IdentityConfigs;

#region Services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
        new SlugifyParameterTransformer()));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(o => o.LowercaseUrls = true);

// add Configuration JSON File
builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("middlewareSettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("connectionStringSetting.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrastractureServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

//var logger = NLog.LogManager
//    .Setup()
//    .LoadConfigurationFromAppSettings()
//    .GetCurrentClassLogger();
//builder.Host.UseNLog();
//builder.Services.AddElmah();

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    b.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

//builder.Services.AddSwaggerGen();
AddSwagger(builder.Services);

#endregion

#region MiddelWares

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();

#endregion

#region SwaggerConfiguration

void AddSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(o =>
    {
        o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 1234sddsw'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        o.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });

        o.SwaggerDoc("v1", new OpenApiInfo()
        {
            Version = "v1",
            Title = "Site Api"
        });
    });
}

#endregion
