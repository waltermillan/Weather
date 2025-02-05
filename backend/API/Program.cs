using API.Extensions; // Aseg�rate de que el espacio de nombres sea correcto
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Agregar AutoMapper
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Configuraci�n de CORS y servicios adicionales (llama a las extensiones aqu�)
builder.Services.ConfigureCors();

// Agregar servicios a la colecci�n
builder.Services.AddHttpClient<WeatherService>();  // Si a�n no lo hab�as agregado
builder.Services.AddControllers();

// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar la tuber�a de solicitudes HTTP
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
        // Puedes agregar cualquier l�gica de inicializaci�n aqu� (por ejemplo, migraciones)
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurri� un error durante la migraci�n");
    }
}

// Usar CORS y otros middlewares
app.UseCors("CorsPolicy"); // Esto aplica la pol�tica CORS configurada
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
