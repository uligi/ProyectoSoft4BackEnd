using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IEquiposProyectosRepository
    {
        Task<IEnumerable<Equipos_Proyectos>> ObtenerEquiposProyectos();
        Task<IEnumerable<MensajeUsuario>> CrearEquipoProyecto(int equiposId, int proyectosId);
        Task<IEnumerable<MensajeUsuario>> ActualizarEquipoProyecto(int equiposId, int proyectosId);
    }

    public class EquiposProyectosRepository : IEquiposProyectosRepository
    {
        private readonly ContextData _context;

        public EquiposProyectosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los equipos y proyectos relacionados
        public async Task<IEnumerable<Equipos_Proyectos>> ObtenerEquiposProyectos()
        {
            return await _context.Equipos_Proyectos.ToListAsync();
        }

        // Método para crear una nueva relación entre un equipo y un proyecto
        public async Task<IEnumerable<MensajeUsuario>> CrearEquipoProyecto(int equiposId, int proyectosId)
        {
            if (equiposId <= 0 || proyectosId <= 0)
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "Los identificadores de equipo y proyecto deben ser válidos" }
                };
            }
            else
            {
                var equiposIdParam = new SqlParameter("@Equipos_idEquipos", equiposId);
                var proyectosIdParam = new SqlParameter("@Proyectos_idProyectos", proyectosId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Equipo_Proyecto @Equipos_idEquipos, @Proyectos_idProyectos", equiposIdParam, proyectosIdParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar una relación entre un equipo y un proyecto
        public async Task<IEnumerable<MensajeUsuario>> ActualizarEquipoProyecto(int equiposId, int proyectosId)
        {
            if (equiposId <= 0 || proyectosId <= 0)
            {
                return new List<MensajeUsuario>
        {
            new MensajeUsuario { Codigo = -3, Mensaje = "Los identificadores de equipo y proyecto deben ser válidos" }
        };
            }
            else
            {
                var equiposIdParam = new SqlParameter("@Equipos_idEquipos", equiposId);
                var proyectosIdParam = new SqlParameter("@Proyectos_idProyectos", proyectosId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Equipo_Proyecto @Equipos_idEquipos, @Proyectos_idProyectos",
                                equiposIdParam, proyectosIdParam)
                    .ToListAsync();
            }
        }

    }
}
