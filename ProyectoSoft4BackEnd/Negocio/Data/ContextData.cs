using Microsoft.EntityFrameworkCore;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Negocio.Data
{
    public class ContextData : DbContext
    {
        public ContextData(DbContextOptions<ContextData> options) : base(options) { }

        public DbSet<Equipos> Equipos { get; set; }
        public DbSet<Portafolio> Portafolio { get; set; }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Subtareas> Subtareas { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesPermisos> RolesPermisos { get; set; }
        public DbSet<MiembrosDeEquipos> MiembrosDeEquipos { get; set; }
        public DbSet<EquiposProyectos> EquiposProyectos { get; set; }
        public DbSet<HistorialDeCambios> HistorialDeCambios { get; set; }
    }
}
