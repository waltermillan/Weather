﻿using Infrastructure.Services;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200")  // Permite solo este origen en desarrollo
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader());
        });

    // Dentro de Startup.cs
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient<WeatherService>();
        // Otras configuraciones de servicios...
    }
}
