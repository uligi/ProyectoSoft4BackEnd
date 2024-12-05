using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface ISubtareasRepository
    {
        Task<IEnumerable<Subtareas>> ListarSubtareas();
        Task<IEnumerable<MensajeUsuario>> CrearSubtarea(SubtareasRequest subtarea);
        Task<IEnumerable<MensajeUsuario>> ActualizarSubtarea(int id, SubtareasRequest subtarea);
        Task<IEnumerable<MensajeUsuario>> EliminarSubtarea(int idSubtareas);
    }

    public class SubtareasRepository : ISubtareasRepository
    {
        private readonly ContextData _context;

        public SubtareasRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subtareas>> ListarSubtareas()
        {
            return await _context.Subtareas
                .FromSqlRaw("EXEC Listar_Subtareas")
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> CrearSubtarea(SubtareasRequest subtarea)
        {
            var parameters = new[]
            {
                new SqlParameter("@NombreSubtareas", subtarea.NombreSubtareas),
                new SqlParameter("@Descripcion", subtarea.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@Prioridad", subtarea.Prioridad ?? (object)DBNull.Value),
                new SqlParameter("@FechaInicio", subtarea.FechaInicio ?? (object)DBNull.Value),
                new SqlParameter("@FechaFinal", subtarea.FechaFinal ?? (object)DBNull.Value),
                new SqlParameter("@idTareas", subtarea.idTareas),
                new SqlParameter("@Estado", subtarea.Estado)
            };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Subtarea @NombreSubtareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @idTareas,@Estado", parameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> ActualizarSubtarea(int id, SubtareasRequest subtarea)
        {
            var parameters = new[]
            {
                new SqlParameter("@idSubtareas", id),
                new SqlParameter("@NombreSubtareas", subtarea.NombreSubtareas),
                new SqlParameter("@Descripcion", subtarea.Descripcion ?? (object)DBNull.Value),
                new SqlParameter("@Prioridad", subtarea.Prioridad ?? (object)DBNull.Value),
                new SqlParameter("@FechaInicio", subtarea.FechaInicio ?? (object)DBNull.Value),
                new SqlParameter("@FechaFinal", subtarea.FechaFinal ?? (object)DBNull.Value),
                new SqlParameter("@idTareas", subtarea.idTareas),
                new SqlParameter("@Estado", subtarea.Estado)
            };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Subtarea @idSubtareas, @NombreSubtareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @idTareas,@Estado", parameters)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> EliminarSubtarea(int idSubtareas)
        {
            var idParam = new SqlParameter("@idSubtareas", idSubtareas);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Subtarea @idSubtareas", idParam)
                .ToListAsync();
        }
    }
}
