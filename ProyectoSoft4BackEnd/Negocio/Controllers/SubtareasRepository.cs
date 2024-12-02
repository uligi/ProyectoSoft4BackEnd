using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface ISubtareasRepository
    {
        Task<IEnumerable<Subtareas>> ObtenerSubtareas();
        Task<IEnumerable<MensajeUsuario>> CrearSubtarea(Subtareas subtarea);
        Task<IEnumerable<MensajeUsuario>> ActualizarSubtarea(int idSubtarea, string nombreSubtarea, string descripcion, string prioridad, System.DateTime fechaInicio, System.DateTime fechaFinal);
    }

    public class SubtareasRepository : ISubtareasRepository
    {
        private readonly ContextData _context;

        public SubtareasRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todas las subtareas
        public async Task<IEnumerable<Subtareas>> ObtenerSubtareas()
        {
            return await _context.Subtareas.ToListAsync();
        }

        // Método para crear una nueva subtarea
        public async Task<IEnumerable<MensajeUsuario>> CrearSubtarea(Subtareas subtarea)
        {
            if (string.IsNullOrEmpty(subtarea.NombreSubtareas))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre de la subtarea no puede estar vacío o nulo" }
                };
            }
            else
            {
                var nombreSubtareaParam = new SqlParameter("@NombreSubtareas", subtarea.NombreSubtareas);
                var descripcionParam = new SqlParameter("@Descripcion", subtarea.Descripcion);
                var prioridadParam = new SqlParameter("@Prioridad", subtarea.Prioridad);
                var fechaInicioParam = new SqlParameter("@FechaInicio", subtarea.FechaInicio);
                var fechaFinalParam = new SqlParameter("@FechaFinal", subtarea.FechaFinal);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Subtarea @NombreSubtareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal",
                                nombreSubtareaParam, descripcionParam, prioridadParam, fechaInicioParam, fechaFinalParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar una subtarea existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarSubtarea(int idSubtarea, string nombreSubtarea, string descripcion, string prioridad, System.DateTime fechaInicio, System.DateTime fechaFinal)
        {
            if (string.IsNullOrEmpty(nombreSubtarea))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre de la subtarea no puede estar vacío o nulo" }
                };
            }
            else
            {
                var idSubtareaParam = new SqlParameter("@idSubtareas", idSubtarea);
                var nombreSubtareaParam = new SqlParameter("@NombreSubtareas", nombreSubtarea);
                var descripcionParam = new SqlParameter("@Descripcion", descripcion);
                var prioridadParam = new SqlParameter("@Prioridad", prioridad);
                var fechaInicioParam = new SqlParameter("@FechaInicio", fechaInicio);
                var fechaFinalParam = new SqlParameter("@FechaFinal", fechaFinal);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Subtarea @idSubtareas, @NombreSubtareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal",
                                idSubtareaParam, nombreSubtareaParam, descripcionParam, prioridadParam, fechaInicioParam, fechaFinalParam)
                    .ToListAsync();
            }
        }
    }
}
