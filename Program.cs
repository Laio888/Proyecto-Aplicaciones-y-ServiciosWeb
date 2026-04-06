// Program.cs - Punto de entrada de la API Knowledge Map
// Configuración de servicios, inyección de dependencias y middleware
// Base de datos: knowledge_map_db
// Tablas Entregable 1 (sin FK): car_innovacion, enfoque, practica_estrategia, aspecto_normativo, universidad

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiKnowledgeMap.Modelos;

// Repositorios
using ApiKnowledgeMap.Repositorios;
using ApiKnowledgeMap.Repositorios.Abstracciones;

// Servicios
using ApiKnowledgeMap.Servicios;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ApiKnowledgeMap.Servicios.Conexion;
using ApiKnowledgeMap.Servicios.Politicas;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// CONFIGURACIÓN ADICIONAL (tablas prohibidas opcionales)
// ---------------------------------------------------------
builder.Configuration.AddJsonFile(
    "tablasprohibidas.json",
    optional: true,
    reloadOnChange: true
);

// ---------------------------------------------------------
// SERVICIOS
// ---------------------------------------------------------

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// CORS — permite consumo desde frontend (cualquier origen en desarrollo)
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("PermitirTodo", politica => politica
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

// Sesión HTTP
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opciones =>
{
    opciones.IdleTimeout = TimeSpan.FromMinutes(30);
    opciones.Cookie.HttpOnly = true;
    opciones.Cookie.IsEssential = true;
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Knowledge Map — Sistema de Gestión del Conocimiento Universitario",
        Version = "v1",
        Description = "API REST genérica para operaciones CRUD sobre knowledge_map_db. " +
                      "Soporta SQL Server, PostgreSQL, MySQL/MariaDB. " +
                      "Entregable 1: car_innovacion, enfoque, practica_estrategia, aspecto_normativo, universidad."
    });

    var esquemaSeguridad = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token con el prefijo 'Bearer'. Ejemplo: Bearer eyJhbGci..."
    };

    opciones.AddSecurityDefinition("Bearer", esquemaSeguridad);
    opciones.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// -----------------------------------------------------------------
// REGISTRO DE SERVICIOS (Dependency Injection - DIP)
// -----------------------------------------------------------------

// ── INFRAESTRUCTURA (Singleton) ───────────────────────────────────

// Política de tablas prohibidas (una instancia para toda la app)
builder.Services.AddSingleton<
    IPoliticaTablasProhibidas,
    PoliticaTablasProhibidasDesdeJson>();

// Proveedor de conexión (comparte cadena de conexión)
builder.Services.AddSingleton<
    IProveedorConexion,
    ProveedorConexion>();

// ── SERVICIO CRUD GENÉRICO (Scoped) ──────────────────────────────
builder.Services.AddScoped<
    IServicioCrud,
    ServicioCrud>();

// ── REPOSITORIO SEGÚN PROVEEDOR DE BD ────────────────────────────
var proveedorBD = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";

switch (proveedorBD.ToLower())
{
    case "postgres":
        throw new InvalidOperationException(
            "El repositorio PostgreSQL aún no está implementado. " +
            "Cambia DatabaseProvider a 'SqlServer' o 'LocalDb' en appsettings.json"
        );

    case "mariadb":
    case "mysql":
        throw new InvalidOperationException(
            "El repositorio MySQL/MariaDB aún no está implementado. " +
            "Cambia DatabaseProvider a 'SqlServer' o 'LocalDb' en appsettings.json"
        );

    case "sqlserver":
    case "sqlserverexpress":
    case "localdb":
    default:
        builder.Services.AddScoped<
            IRepositorioLecturaTabla,
            RepositorioLecturaSqlServer>();
        break;
}

// ── ENTIDADES ESPECÍFICAS (Scoped) ───────────────────────────────
// Patrón: primero el Repositorio, luego el Servicio que lo usa.

// Universidad
builder.Services.AddScoped<IUniversidadRepository, UniversidadRepository>();
builder.Services.AddScoped<IUniversidadService, UniversidadService>();

// CarInnovacion
builder.Services.AddScoped<ICarInnovacionRepository, CarInnovacionRepository>();
builder.Services.AddScoped<ICarInnovacionService, CarInnovacionService>();

// PracticaEstrategia
builder.Services.AddScoped<IPracticaEstrategiaRepository, PracticaEstrategiaRepository>();
builder.Services.AddScoped<IPracticaEstrategiaService, PracticaEstrategiaService>();

// Enfoque
builder.Services.AddScoped<IEnfoqueRepository, EnfoqueRepository>();
builder.Services.AddScoped<IEnfoqueService, EnfoqueService>();

// AspectosNormativos
builder.Services.AddScoped<IAspectoNormativoRepository, AspectoNormativoRepository>();
builder.Services.AddScoped<IAspectoNormativoService, AspectoNormativoService>();

//Facultad
builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();
builder.Services.AddScoped<IFacultadService, FacultadService>();

//Programa
builder.Services.AddScoped<IProgramaRepository, ProgramaRepository>();
builder.Services.AddScoped<IProgramaService, ProgramaService>();

builder.Services.AddScoped<IAaRcRepository, AaRcRepository>();
builder.Services.AddScoped<IAaRcService, AaRcService>();

//EnfoqueRc
builder.Services.AddScoped<IEnfoqueRcRepository, EnfoqueRcRepository>();
builder.Services.AddScoped<IEnfoqueRcService, EnfoqueRcService>();

//Registro Calificado
builder.Services.AddScoped<IRegistroCalificadoRepository, RegistroCalificadoRepository>();
builder.Services.AddScoped<IRegistroCalificadoService, RegistroCalificadoService>();

// ---------------------------------------------------------
// CONFIGURACIÓN JWT
// ---------------------------------------------------------
builder.Services.Configure<ConfiguracionJwt>(
    builder.Configuration.GetSection("Jwt")
);

var configuracionJwt = new ConfiguracionJwt();
builder.Configuration.GetSection("Jwt").Bind(configuracionJwt);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuracionJwt.Issuer,
            ValidAudience = configuracionJwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuracionJwt.Key)
            )
        };
    });

var app = builder.Build();

// ---------------------------------------------------------
// MIDDLEWARE (el orden importa)
// ---------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Swagger disponible en /swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiKnowledgeMap v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors("PermitirTodo");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.MapControllers();
app.Run();
app.Run();