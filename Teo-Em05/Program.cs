using Microsoft.EntityFrameworkCore;
using Teo_Em05.Data;
using Teo_Em05.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- SECCIÓN DE CONFIGURACIÓN DE SERVICIOS ---

// 1. Configuración de la Conexión a la Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RestauranteDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Inyección de Dependencias para los Repositorios (el "contrato" se conecta con la "cocina")
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
// A medida que crees más repositorios, los agregarás aquí. Ejemplo:
// builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
// builder.Services.AddScoped<IMesaRepository, MesaRepository>();


// 3. Habilitar el uso de Controladores para la API
builder.Services.AddControllers();

// 4. Configuración de Swagger/OpenAPI para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- FIN DE LA SECCIÓN DE CONFIGURACIÓN ---


var app = builder.Build();

// --- SECCIÓN DE CONFIGURACIÓN DEL PIPELINE HTTP ---

// Configura el pipeline de peticiones HTTP.
if (app.Environment.IsDevelopment())
{
    // Habilita la interfaz de Swagger solo en el entorno de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige las peticiones HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita la autorización (lo necesitaremos más adelante para la seguridad)
app.UseAuthorization();

// Mapea las rutas definidas en tus controladores
app.MapControllers();

// --- FIN DE LA SECCIÓN DEL PIPELINE ---

// Inicia la aplicación
app.Run();