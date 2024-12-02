using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IHistorialDeCambiosRepository
    {
        Task<IEnumerable<Historial_de_cambios>> ObtenerHistorialDeCambios();
        Task<IEnumerable<MensajeUsuario>> CrearHistorialDeCambio(Historial_de_cambios historial);
        Task<IEnumerable<MensajeUsuario>> ActualizarHistorialDeCambio(int idHistorial, string descripcionCambio, System.DateTime fechaCambio);
    }

    public class HistorialDeCambiosRepository : IHistorialDeCambiosRepository
    {
        private readonly ContextData _context;

        public HistorialDeCambiosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los registros de Historial de Cambios
        public async Task<IEnumerable<Historial_de_cambios>> ObtenerHistorialDeCambios()
        {
            return await _context.Historial_de_cambios.ToListAsync();
        }

        // Método para crear un nuevo registro de Historial de Cambios
        public async Task<IEnumerable<MensajeUsuario>> CrearHistorialDeCambio(Historial_de_cambios historial)
        {
            if (string.IsNullOrEmpty(historial.Descripcioncambio))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "La descripción del cambio no puede estar vacía o nula" }
                };
            }
            else
            {
                var tareasIdParam = new SqlParameter("@Tareas_idTareas", historial.Tareas_idTareas);
                var proyectosIdParam = new SqlParameter("@Proyectos_idProyectos", historial.Proyectos_idProyectos);
                var portafolioIdParam = new SqlParameter("@Portafolio_idPortafolio", historial.Portafolio_idPortafolio);
                var descripcionCambioParam = new SqlParameter("@DescripcionCambio", historial.Descripcioncambio);
                var fechaCambioParam = new SqlParameter("@FechaCambio", historial.FechaCambio);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_HistorialCambio @Tareas_idTareas, @Proyectos_idProyectos, @Portafolio_idPortafolio, @DescripcionCambio, @FechaCambio",
                                tareasIdParam, proyectosIdParam, portafolioIdParam, descripcionCambioParam, fechaCambioParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un registro de Historial de Cambios
        public async Task<IEnumerable<MensajeUsuario>> ActualizarHistorialDeCambio(int idHistorial, string descripcionCambio, System.DateTime fechaCambio)
        {
            if (string.IsNullOrEmpty(descripcionCambio))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "La descripción del cambio no puede estar vacía o nula" }
                };
            }
            else
            {
                var idHistorialParam = new SqlParameter("@idHistorial_de_cambios", idHistorial);
                var descripcionCambioParam = new SqlParameter("@DescripcionCambio", descripcionCambio);
                var fechaCambioParam = new SqlParameter("@FechaCambio", fechaCambio);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_HistorialCambio @idHistorial_de_cambios, @DescripcionCambio, @FechaCambio",
                                idHistorialParam, descripcionCambioParam, fechaCambioParam)
                    .ToListAsync();
            }
        }
    }
}
