using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaCreditos.Application.Interfaces.ExternalServices;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.Security;
using SistemaCreditos.Application.UseCases.Estudiantes;
using SistemaCreditos.Application.UseCases.Materias;
using SistemaCreditos.Application.UseCases.Profesores;
using SistemaCreditos.Application.UseCases.Usuarios;
using SistemaCreditos.Infrastructure.ExternalServices;
using SistemaCreditos.Infrastructure.Persistence.Context;
using SistemaCreditos.Infrastructure.Persistence.Repositories;
using SistemaCreditos.Infrastructure.Security;
using System.Text;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Configurar Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configuramos el "Authorize"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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
builder.Services.AddScoped<InscribirMateriasUC>();
builder.Services.AddScoped<RegistrarMateriaUC>();
builder.Services.AddScoped<VerCompañerosClaseUC>();
builder.Services.AddScoped<ObtenerMateriasDisponiblesUC>();
builder.Services.AddScoped<CancelarInscripcionUC>();
builder.Services.AddScoped<VerPerfilEstudianteUC>();
builder.Services.AddScoped<ObtenerSolicitudesPendientesUC>();
builder.Services.AddScoped<ProcesarSolicitudProfesorUC>();
//builder.Services.AddScoped<IEmailService, FakeEmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<RepostularProfesorUC>();
builder.Services.AddScoped<ActualizarDatosUsuarioUC>();
builder.Services.AddScoped<ActualizarUsuarioAdminUC>();
builder.Services.AddScoped<ObtenerTodosLosEstudiantesUC>();

// Configuracion del HealtCheck

builder.Services.AddHealthChecks()
    .AddCheck("Self", () => HealthCheckResult.Healthy("La API está viva y respondiendo."))
    .AddDbContextCheck<AppDbContext>(
        name: "Database",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "mysql" }
    );

var secretKey = builder.Configuration["JwtSettings:SecretKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configurar el entorno HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
