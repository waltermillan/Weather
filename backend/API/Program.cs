using API.Extensions; // Asegúrate de que el espacio de nombres sea correcto
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Agregar AutoMapper
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Configuración de CORS y servicios adicionales (llama a las extensiones aquí)
builder.Services.ConfigureCors();

// Agregar servicios a la colección
builder.Services.AddHttpClient<WeatherService>();  // Si aún no lo habías agregado
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar la tubería de solicitudes HTTP
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
        // Puedes agregar cualquier lógica de inicialización aquí (por ejemplo, migraciones)
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrió un error durante la migración");
    }
}

// Usar CORS y otros middlewares
app.UseCors("CorsPolicy"); // Esto aplica la política CORS configurada
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
