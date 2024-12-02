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
        Task<IEnumerable<Tareas>> ObtenerTareas();
        Task<IEnumerable<MensajeUsuario>> CrearTarea(Tareas tarea);
        Task<IEnumerable<MensajeUsuario>> ActualizarTarea(int idTarea, string nombreTarea, string descripcion, string prioridad, System.DateTime fechaInicio, System.DateTime fechaFinal, bool activo, int subtareaId, int proyectoId, int comentarioId);
    }

    public class TareasRepository : ITareasRepository
    {
        private readonly ContextData _context;

        public TareasRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todas las tareas
        public async Task<IEnumerable<Tareas>> ObtenerTareas()
        {
            return await _context.Tareas.ToListAsync();
        }

        // Método para crear una nueva tarea
        public async Task<IEnumerable<MensajeUsuario>> CrearTarea(Tareas tarea)
        {
            if (string.IsNullOrEmpty(tarea.NombreTareas))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre de la tarea no puede estar vacío o nulo" }
                };
            }
            else
            {
                var nombreTareaParam = new SqlParameter("@NombreTareas", tarea.NombreTareas);
                var descripcionParam = new SqlParameter("@Descripcion", tarea.Descripcion);
                var prioridadParam = new SqlParameter("@Prioridad", tarea.Prioridad);
                var fechaInicioParam = new SqlParameter("@FechaInicio", tarea.FechaInicio);
                var fechaFinalParam = new SqlParameter("@FechaFinal", tarea.FechaFinal);
                var activoParam = new SqlParameter("@Activo", tarea.Activo);
                var subtareasIdParam = new SqlParameter("@Subtareas_idSubtareas", tarea.Subtareas_idSubtareas);
                var proyectosIdParam = new SqlParameter("@Proyectos_idProyectos", tarea.Proyectos_idProyectos);
                var comentariosIdParam = new SqlParameter("@Comentarios_idComentarios", tarea.Comentarios_idComentarios);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Tarea @NombreTareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @Activo, @Subtareas_idSubtareas, @Proyectos_idProyectos, @Comentarios_idComentarios",
                                nombreTareaParam, descripcionParam, prioridadParam, fechaInicioParam, fechaFinalParam, activoParam, subtareasIdParam, proyectosIdParam, comentariosIdParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar una tarea existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarTarea(int idTarea, string nombreTarea, string descripcion, string prioridad, System.DateTime fechaInicio, System.DateTime fechaFinal, bool activo, int subtareaId, int proyectoId, int comentarioId)
        {
            if (string.IsNullOrEmpty(nombreTarea))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre de la tarea no puede estar vacío o nulo" }
                };
            }
            else
            {
                var idTareaParam = new SqlParameter("@idTareas", idTarea);
                var nombreTareaParam = new SqlParameter("@NombreTareas", nombreTarea);
                var descripcionParam = new SqlParameter("@Descripcion", descripcion);
                var prioridadParam = new SqlParameter("@Prioridad", prioridad);
                var fechaInicioParam = new SqlParameter("@FechaInicio", fechaInicio);
                var fechaFinalParam = new SqlParameter("@FechaFinal", fechaFinal);
                var activoParam = new SqlParameter("@Activo", activo);
                var subtareaIdParam = new SqlParameter("@Subtareas_idSubtareas", subtareaId);
                var proyectoIdParam = new SqlParameter("@Proyectos_idProyectos", proyectoId);
                var comentarioIdParam = new SqlParameter("@Comentarios_idComentarios", comentarioId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Tarea @idTareas, @NombreTareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @Activo, @Subtareas_idSubtareas, @Proyectos_idProyectos, @Comentarios_idComentarios",
                                idTareaParam, nombreTareaParam, descripcionParam, prioridadParam, fechaInicioParam, fechaFinalParam, activoParam, subtareaIdParam, proyectoIdParam, comentarioIdParam)
                    .ToListAsync();
            }
        }
    }
}
