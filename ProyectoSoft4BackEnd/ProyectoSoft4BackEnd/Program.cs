using Microsoft.EntityFrameworkCore;
using Negocio.Controllers;
using Negocio.Controllers.Negocio.Controllers;
using Negocio.Data;
using Negocio.Modelos;
using Negocio.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContextData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IProyectosRepository, ProyectosRepository>();
builder.Services.AddScoped<ISubtareasRepository, SubtareasRepository>();
builder.Services.AddScoped<IComentariosRepository, ComentariosRepository>();
builder.Services.AddScoped<ITareasRepository, TareasRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IPermisosRepository, PermisosRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();

builder.Services.AddScoped<IMiembrosDeEquiposRepository, MiembrosRepository>();

builder.Services.AddScoped<IHistorialDeCambiosRepository, HistorialDeCambiosRepository>();
builder.Services.AddScoped<IPortafolioRepository, PortafolioRepository>();
builder.Services.AddScoped<IEquiposRepository, EquiposRepository>();
builder.Services.AddScoped<IRecursosRepository, RecursosRepository>();


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
app.UseAuthorization();

app.MapControllers();

app.Run();
