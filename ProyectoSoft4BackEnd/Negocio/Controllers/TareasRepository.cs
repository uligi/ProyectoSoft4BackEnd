using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface ITareasRepository
    {
        Task<IEnumerable<TareasResponse>> ListarTareas();
        Task<IEnumerable<MensajeUsuario>> CrearTarea(TareasRequest tarea);
        Task<IEnumerable<MensajeUsuario>> ActualizarTarea(TareasRequest tarea);
        Task<IEnumerable<MensajeUsuario>> EliminarTarea(int idTareas);
        Task<IEnumerable<TareasResponse>> ListarTareasPorProyecto(int idProyectos);

        // Métodos nuevos
        Task<TareasResponse> ObtenerTareaPorID(int idTarea);
        Task<IEnumerable<Subtareas>> ObtenerSubtareasPorTareaID(int idTarea);

        Task<IEnumerable<TareasResponse>> ListarTareasPorUsuario(int idUsuario);

    }

    public class TareasRepository : ITareasRepository
    {
        private readonly ContextData _context;

        public TareasRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TareasResponse>> ListarTareas()
        {
            return await _context.Tareas
                .FromSqlRaw("EXEC Listar_Tareas")
                .ToListAsync();
        }
        public async Task<IEnumerable<TareasResponse>> ListarTareasPorProyecto(int idProyectos)
        {
            var idParam = new SqlParameter("@idProyectos", idProyectos);

            return await _context.Tareas
                .FromSqlRaw("EXEC Listar_Tareas_Por_Proyecto @idProyectos", idParam)
                .ToListAsync();
        }

        // Nuevo método: Obtener Tarea por ID
        public async Task<TareasResponse> ObtenerTareaPorID(int idTarea)
        {
            var idParam = new SqlParameter("@idTarea", idTarea);

            var result = await _context.Tareas
                .FromSqlRaw("EXEC sp_ObtenerTareaPorID @idTarea", idParam)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }


        // Nuevo método: Obtener Subtareas por Tarea ID
        public async Task<IEnumerable<Subtareas>> ObtenerSubtareasPorTareaID(int idTarea)
        {
            var idParam = new SqlParameter("@idTarea", idTarea);

            return await _context.Subtareas
                .FromSqlRaw("EXEC sp_ObtenerSubtareasPorIDTarea @idTarea", idParam)
                .ToListAsync();
        }
        public async Task<IEnumerable<MensajeUsuario>> CrearTarea(TareasRequest tarea)
        {
            var parameters = new[]
            {
            new SqlParameter("@NombreTareas", tarea.NombreTareas),
            new SqlParameter("@Descripcion", tarea.Descripcion ?? (object)DBNull.Value),
            new SqlParameter("@Prioridad", tarea.Prioridad ?? (object)DBNull.Value),
            new SqlParameter("@FechaInicio", tarea.FechaInicio ?? (object)DBNull.Value),
            new SqlParameter("@FechaFinal", tarea.FechaFinal ?? (object)DBNull.Value),
            new SqlParameter("@idProyectos", tarea.idProyectos),
            new SqlParameter("@idUsuarios", tarea.idUsuarios ?? (object)DBNull.Value),
             new SqlParameter("@Estado", tarea.Estado)
        };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Tarea @NombreTareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @idProyectos, @idUsuarios, @Estado", parameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> ActualizarTarea(TareasRequest tarea)
        {
            var parameters = new[]
            {
            new SqlParameter("@idTareas", tarea.idTareas),
            new SqlParameter("@NombreTareas", tarea.NombreTareas),
            new SqlParameter("@Descripcion", tarea.Descripcion ?? (object)DBNull.Value),
            new SqlParameter("@Prioridad", tarea.Prioridad ?? (object)DBNull.Value),
            new SqlParameter("@FechaInicio", tarea.FechaInicio ?? (object)DBNull.Value),
            new SqlParameter("@FechaFinal", tarea.FechaFinal ?? (object)DBNull.Value),
            new SqlParameter("@idProyectos", tarea.idProyectos),
            new SqlParameter("@idUsuarios", tarea.idUsuarios ?? (object)DBNull.Value),
            new SqlParameter("@Estado", tarea.Estado) 
        };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Tarea @idTareas, @NombreTareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @idProyectos, @idUsuarios, @Estado", parameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> EliminarTarea(int idTareas)
        {
            var idParam = new SqlParameter("@idTareas", idTareas);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Tarea @idTareas", idParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<TareasResponse>> ListarTareasPorUsuario(int idUsuario)
        {
            var idParam = new SqlParameter("@idUsuario", idUsuario);

            return await _context.Tareas
                .FromSqlRaw("EXEC ListarTareasPorUsuario @idUsuario", idParam)
                .ToListAsync();
        }

    }
}
