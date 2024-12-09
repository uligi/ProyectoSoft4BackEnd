using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Negocio.Controllers;
using Negocio.Data;
using Negocio.Repositories;
using Negocio.Controllers.Negocio.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContextData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IProyectosRepository, ProyectosRepository>();
builder.Services.AddScoped<ISubtareasRepository, SubtareasRepository>();
builder.Services.AddScoped<IComentariosProyectosRepository, ComentariosProyectosRepository>();
builder.Services.AddScoped<IComentariosSubtareasRepository, ComentariosSubtareasRepository>();
builder.Services.AddScoped<IComentariosTareasRepository, ComentariosTareasRepository>();
builder.Services.AddScoped<ITareasRepository, TareasRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IPermisosRepository, PermisosRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IMiembrosDeEquiposRepository, MiembrosRepository>();
builder.Services.AddScoped<IHistorialDeCambiosRepository, HistorialDeCambiosRepository>();
builder.Services.AddScoped<IPortafolioRepository, PortafolioRepository>();
builder.Services.AddScoped<IEquiposRepository, EquiposRepository>();
builder.Services.AddScoped<IRecursosRepository, RecursosRepository>();

// Registro de ReportesRepository
builder.Services.AddScoped<ReportesRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new ReportesRepository(connectionString);
});

// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "tuapi.com",
        ValidAudience = "tuapi.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ClaveSuperSecretaParaJWT"))
    };
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFullAcceso", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configuración de controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración de entorno y Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFullAcceso");

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
