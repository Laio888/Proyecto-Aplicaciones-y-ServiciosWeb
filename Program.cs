using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ApiKnowledgeMap.Servicios.Conexion;
using ApiKnowledgeMap.Servicios.Politicas;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// CONFIGURACIÓN ADICIONAL
// ---------------------------------------------------------
builder.Configuration.AddJsonFile(
    "tablasprohibidas.json",
    optional: true,
    reloadOnChange: true
);

// ---------------------------------------------------------
// SERVICIOS BASE
// ---------------------------------------------------------
builder.Services.AddControllers();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("PermitirTodo", politica => politica
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opciones =>
{
    opciones.IdleTimeout = TimeSpan.FromMinutes(30);
    opciones.Cookie.HttpOnly = true;
    opciones.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Knowledge Map",
        Version = "v1",
        Description = "API REST para el Sistema de Gestión del Conocimiento Universitario."
    });

    var esquemaSeguridad = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token con el prefijo 'Bearer'."
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

// ---------------------------------------------------------
// INFRAESTRUCTURA (Singleton)
// ---------------------------------------------------------
builder.Services.AddSingleton<IPoliticaTablasProhibidas, PoliticaTablasProhibidasDesdeJson>();
builder.Services.AddSingleton<IProveedorConexion, ProveedorConexion>();

// ---------------------------------------------------------
// SERVICIO CRUD GENÉRICO
// ---------------------------------------------------------
builder.Services.AddScoped<IServicioCrud, ServicioCrud>();

// ---------------------------------------------------------
// REPOSITORIO SEGÚN PROVEEDOR DE BD
// ---------------------------------------------------------
var proveedorBD = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";

if (proveedorBD.ToLower() == "sqlserver" ||
    proveedorBD.ToLower() == "sqlserverexpress" ||
    proveedorBD.ToLower() == "localdb")
{
    builder.Services.AddScoped<IRepositorioLecturaTabla, RepositorioLecturaSqlServer>();
}

// ---------------------------------------------------------
// ENTREGABLE 1 - Sin FK
// ---------------------------------------------------------
builder.Services.AddScoped<IUniversidadRepository, UniversidadRepository>();
builder.Services.AddScoped<IUniversidadService, UniversidadService>();

builder.Services.AddScoped<ICarInnovacionRepository, CarInnovacionRepository>();
builder.Services.AddScoped<ICarInnovacionService, CarInnovacionService>();

builder.Services.AddScoped<IPracticaEstrategiaRepository, PracticaEstrategiaRepository>();
builder.Services.AddScoped<IPracticaEstrategiaService, PracticaEstrategiaService>();

builder.Services.AddScoped<IEnfoqueRepository, EnfoqueRepository>();
builder.Services.AddScoped<IEnfoqueService, EnfoqueService>();

builder.Services.AddScoped<IAspectoNormativoRepository, AspectoNormativoRepository>();
builder.Services.AddScoped<IAspectoNormativoService, AspectoNormativoService>();

// ---------------------------------------------------------
// ENTREGABLE 2 - Con FK simples
// ---------------------------------------------------------
builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();
builder.Services.AddScoped<IFacultadService, FacultadService>();

builder.Services.AddScoped<IProgramaRepository, ProgramaRepository>();
builder.Services.AddScoped<IProgramaService, ProgramaService>();

builder.Services.AddScoped<IPasantiaRepository, PasantiaRepository>();
builder.Services.AddScoped<IPasantiaService, PasantiaService>();

builder.Services.AddScoped<IPremioRepository, PremioRepository>();
builder.Services.AddScoped<IPremioService, PremioService>();

builder.Services.AddScoped<IAreaConocimientoRepository, AreaConocimientoRepository>();
builder.Services.AddScoped<IAreaConocimientoService, AreaConocimientoService>();

// ---------------------------------------------------------
// RELACIONES N:M
// ---------------------------------------------------------
builder.Services.AddScoped<IProgramaAcRepository, ProgramaAcRepository>();
builder.Services.AddScoped<IProgramaAcService, ProgramaAcService>();

builder.Services.AddScoped<IProgramaCiRepository, ProgramaCiRepository>();
builder.Services.AddScoped<IProgramaCiService, ProgramaCiService>();

builder.Services.AddScoped<IProgramaPeRepository, ProgramaPeRepository>();
builder.Services.AddScoped<IProgramaPeService, ProgramaPeService>();

// ---------------------------------------------------------
// ENTREGABLE 3 - Usuarios y Roles
// ---------------------------------------------------------
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioRolRepository, UsuarioRolRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// ---------------------------------------------------------
// JWT
// ---------------------------------------------------------
builder.Services.Configure<ConfiguracionJwt>(
    builder.Configuration.GetSection("ConfiguracionJwt")
);

var jwt = builder.Configuration
    .GetSection("ConfiguracionJwt")
    .Get<ConfiguracionJwt>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt.Key)
            )
        };
    });

// ---------------------------------------------------------
// BUILD
// ---------------------------------------------------------
var app = builder.Build();

// ---------------------------------------------------------
// MIDDLEWARE
// ---------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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
app.Run();