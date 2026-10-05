using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.Security;
using SistemaCreditos.Application.UseCases.Usuarios;
using SistemaCreditos.Infrastructure.Persistence.Context;
using SistemaCreditos.Infrastructure.Persistence.Repositories;
using SistemaCreditos.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Configurar Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la Base de Datos (MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Conectar Interfaces con Implementaciones
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Casos de Uso
builder.Services.AddScoped<RegistrarUsuarioUC>();
builder.Services.AddScoped<LoginUC>();

var app = builder.Build();

// Configurar el entorno HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
