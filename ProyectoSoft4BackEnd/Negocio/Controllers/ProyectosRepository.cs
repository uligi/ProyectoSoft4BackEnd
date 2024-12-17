using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IProyectosRepository
    {
        Task<IEnumerable<Proyectos>> ObtenerProyectos();
        Task<IEnumerable<MensajeUsuario>> CrearProyecto(Proyectos proyecto);
        Task<IEnumerable<MensajeUsuario>> ActualizarProyecto(Proyectos proyecto);
        Task<IEnumerable<MensajeUsuario>> EliminarProyecto(int idProyectos);
        Task<IEnumerable<Proyectos>> ObtenerProyectosPorUsuario(int idUsuario);
    }

    public class ProyectosRepository : IProyectosRepository
    {
        private readonly ContextData _context;

        public ProyectosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proyectos>> ObtenerProyectos()
        {
            return await _context.Proyectos.ToListAsync();
        }

        public async Task<IEnumerable<Proyectos>> ObtenerProyectosPorUsuario(int idUsuario)
        {
            var idParam = new SqlParameter("@idUsuario", idUsuario);

            return await _context.Proyectos
                .FromSqlRaw("EXEC ListarProyectosPorUsuario @idUsuario", idParam)
                .ToListAsync();
        }


        public async Task<IEnumerable<MensajeUsuario>> CrearProyecto(Proyectos proyecto)
        {
            var parameters = new[]
            {
                new SqlParameter("@NombreProyecto", proyecto.NombreProyecto),
                new SqlParameter("@Descripcion", proyecto.Descripcion),
                new SqlParameter("@FechaEstimada", proyecto.FechaEstimada),
                new SqlParameter("@FechaInicio", proyecto.FechaInicio ?? (object)DBNull.Value),
                new SqlParameter("@FechaFinal", proyecto.FechaFinal ?? (object)DBNull.Value),
                new SqlParameter("@Prioridad", proyecto.Prioridad),
                new SqlParameter("@idPortafolio", proyecto.idPortafolio),
                new SqlParameter("@Equipos_idEquipos", proyecto.Equipos_idEquipos),
                 new SqlParameter("@Estado", proyecto.Estado)
            };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Proyecto @NombreProyecto, @Descripcion, @FechaEstimada, @FechaInicio, @FechaFinal, @Prioridad, @idPortafolio, @Equipos_idEquipos ,@Estado", parameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> ActualizarProyecto(Proyectos proyecto)
        {
            var parameters = new[]
            {
                new SqlParameter("@idProyectos", proyecto.idProyectos),
                new SqlParameter("@NombreProyecto", proyecto.NombreProyecto),
                new SqlParameter("@Descripcion", proyecto.Descripcion),
                new SqlParameter("@FechaEstimada", proyecto.FechaEstimada),
                new SqlParameter("@FechaInicio", proyecto.FechaInicio ?? (object)DBNull.Value),
                new SqlParameter("@FechaFinal", proyecto.FechaFinal ?? (object)DBNull.Value),
                new SqlParameter("@Prioridad", proyecto.Prioridad),
                new SqlParameter("@idPortafolio", proyecto.idPortafolio),
                new SqlParameter("@Equipos_idEquipos", proyecto.Equipos_idEquipos),
                 new SqlParameter("@Estado", proyecto.Estado)
            };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Proyecto @idProyectos, @NombreProyecto, @Descripcion, @FechaEstimada, @FechaInicio, @FechaFinal, @Prioridad, @idPortafolio, @Equipos_idEquipos, @Estado", parameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> EliminarProyecto(int idProyectos)
        {
            var idParam = new SqlParameter("@idProyectos", idProyectos);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Proyecto @idProyectos", idParam)
                .ToListAsync();
        }
    }
}
