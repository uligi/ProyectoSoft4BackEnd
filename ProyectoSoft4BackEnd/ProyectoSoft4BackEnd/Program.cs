using Microsoft.EntityFrameworkCore;
using Negocio.Controllers;
using Negocio.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProyectosRepository, ProyectosRepository>();
builder.Services.AddScoped<ISubtareasRepository, SubtareasRepository>();
builder.Services.AddScoped<IComentariosRepository, ComentariosRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFullAcceso", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFullAcceso");
app.UseAuthorization();

app.MapControllers();

app.Run();
