using API.Extensions;
using API.Services;
using Core.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var appName = builder.Configuration["SystemName:AppName"] ?? "WeatherApp";

// Clean up other logging providers
builder.Logging.ClearProviders();

// Configurar filtros de logging para ASP.NET Core y Entity Framework Core
builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // Filters for the entire Microsoft part
builder.Logging.AddFilter("System", LogLevel.Warning); // Filters for System logs

// Configuring Serilog to write to file only
Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"logs/{appName}-.log", rollingInterval: RollingInterval.Day) // Log in daily archive
    .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore")) // Exclude logs from Entity Framework Core
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore")) // Exclude logs from ASP.NET Core
    .CreateLogger();

builder.Logging.AddSerilog(); // Add Serilog as a log provider

builder.Services.Configure<EncryptionSettings>(
    builder.Configuration.GetSection("EncryptionSettings"));

// Agregar AutoMapper
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// CORS configuration and additional services (call extensions here)
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddDbContext<Context>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DbConnection")));

// Force automatically generated paths (as with [controller]) to be lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Adding services to the collection
builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuring the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrió un error durante la migración");
    }
}

var policyName = builder.Configuration.GetSection("CorsSettings:PolicyName").Get<string>();

// Using CORS and other middleware
app.UseCors(policyName);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
