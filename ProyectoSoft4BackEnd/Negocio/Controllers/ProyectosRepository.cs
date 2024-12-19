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
        Task<IEnumerable<Proyectos2>> ObtenerProyectosPorPortafolio(int idPortafolio);
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

        public async Task<IEnumerable<Proyectos2>> ObtenerProyectosPorPortafolio(int idPortafolio)
        {
            var idParam = new SqlParameter("@idPortafolio", idPortafolio);

            // Ejecuta la consulta y obtén la lista como el modelo Proyectos
            var proyectos = await _context.Proyectos2
                .FromSqlRaw("EXEC sp_Listar_Proyectos_Por_Portafolio @idPortafolio", idParam)
                .ToListAsync();

            // Mapea manualmente cada proyecto a Proyectos2
            return proyectos.Select(p => new Proyectos2
            {
                idProyectos = p.idProyectos,
                NombreProyecto = p.NombreProyecto,
                Descripcion = p.Descripcion,
                Activo = p.Activo,
                FechaEstimada = p.FechaEstimada,
                FechaInicio = p.FechaInicio,
                FechaFinal = p.FechaFinal,
                Prioridad = p.Prioridad,
                idPortafolio = p.idPortafolio,
                Equipos_idEquipos = p.Equipos_idEquipos,
                Estado = p.Estado,
                NombreEquipos = p.NombreEquipos // Asegúrate de que este campo está disponible en Proyectos
            }).ToList();
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
