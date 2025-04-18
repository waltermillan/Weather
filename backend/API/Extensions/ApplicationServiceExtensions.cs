using API.Services;
using Core.Interfaces;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration) =>
        services.AddCors(options =>
        {
            string[] verbs = configuration.GetSection("CorsSettings:Methods").Get<string[]>();

            var origins = configuration.GetSection("CorsSettings:Origins").Get<string[]>();
            var policyName = configuration.GetSection("CorsSettings:PolicyName").Get<string>();

            options.AddPolicy(policyName, builder =>
                builder.WithOrigins(origins)  // Allows only these developing origins
                    .WithMethods(verbs)
                    .AllowAnyHeader());
        });

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<WeatherService>();
        services.AddScoped<HistoricalQueryDTOService>();
        services.AddSingleton<AesEncryptionService>();
        services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHistoricalQueryRepository, HistoricalQueryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
    }
}
