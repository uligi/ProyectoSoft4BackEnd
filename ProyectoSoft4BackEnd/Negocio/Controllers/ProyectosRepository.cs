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
        Task<IEnumerable<MensajeUsuario>> ActualizarProyecto(int idProyecto, string nombreProyecto, string descripcion, bool activo, string prioridad, System.DateTime fechaEstimada, System.DateTime fechaInicio, System.DateTime fechaFinal, int portafolioId);
    }

    public class ProyectosRepository : IProyectosRepository
    {
        private readonly ContextData _context;

        public ProyectosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los proyectos
        public async Task<IEnumerable<Proyectos>> ObtenerProyectos()
        {
            return await _context.Proyectos.ToListAsync();
        }

        // Método para crear un nuevo proyecto
        public async Task<IEnumerable<MensajeUsuario>> CrearProyecto(Proyectos proyecto)
        {
            if (string.IsNullOrEmpty(proyecto.NombreProyecto))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del proyecto no puede estar vacío o nulo" }
                };
            }
            else
            {
                var nombreProyectoParam = new SqlParameter("@NombreProyecto", proyecto.NombreProyecto);
                var descripcionParam = new SqlParameter("@Descripcion", proyecto.Descripcion);
                var activoParam = new SqlParameter("@Activo", proyecto.Activo);
                var prioridadParam = new SqlParameter("@Prioridad", proyecto.Prioridad);
                var fechaEstimadaParam = new SqlParameter("@FechaEstimada", proyecto.FechaEstimada);
                var fechaInicioParam = new SqlParameter("@FechaInicio", proyecto.FechaInicio);
                var fechaFinalParam = new SqlParameter("@FechaFinal", proyecto.FechaFinal);
                var portafolioIdParam = new SqlParameter("@Portafolio_idPortafolio", proyecto.Portafolio_idPortafolio);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Proyecto @NombreProyecto, @Descripcion, @Activo, @Prioridad, @FechaEstimada, @FechaInicio, @FechaFinal, @Portafolio_idPortafolio",
                                nombreProyectoParam, descripcionParam, activoParam, prioridadParam, fechaEstimadaParam, fechaInicioParam, fechaFinalParam, portafolioIdParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un proyecto existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarProyecto(int idProyecto, string nombreProyecto, string descripcion, bool activo, string prioridad, System.DateTime fechaEstimada, System.DateTime fechaInicio, System.DateTime fechaFinal, int portafolioId)
        {
            if (string.IsNullOrEmpty(nombreProyecto))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del proyecto no puede estar vacío o nulo" }
                };
            }
            else
            {
                var idProyectoParam = new SqlParameter("@idProyectos", idProyecto);
                var nombreProyectoParam = new SqlParameter("@NombreProyecto", nombreProyecto);
                var descripcionParam = new SqlParameter("@Descripcion", descripcion);
                var activoParam = new SqlParameter("@Activo", activo);
                var prioridadParam = new SqlParameter("@Prioridad", prioridad);
                var fechaEstimadaParam = new SqlParameter("@FechaEstimada", fechaEstimada);
                var fechaInicioParam = new SqlParameter("@FechaInicio", fechaInicio);
                var fechaFinalParam = new SqlParameter("@FechaFinal", fechaFinal);
                var portafolioIdParam = new SqlParameter("@Portafolio_idPortafolio", portafolioId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Proyecto @idProyectos, @NombreProyecto, @Descripcion, @Activo, @Prioridad, @FechaEstimada, @FechaInicio, @FechaFinal, @Portafolio_idPortafolio",
                                idProyectoParam, nombreProyectoParam, descripcionParam, activoParam, prioridadParam, fechaEstimadaParam, fechaInicioParam, fechaFinalParam, portafolioIdParam)
                    .ToListAsync();
            }
        }
    }
}
