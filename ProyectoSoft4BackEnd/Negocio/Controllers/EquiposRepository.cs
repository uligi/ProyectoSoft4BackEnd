using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IEquiposRepository
    {
        Task<IEnumerable<Equipos>> ObtenerEquipos();
        Task<IEnumerable<MensajeUsuario>> CrearEquipo(Equipos equipo);
        Task<IEnumerable<MensajeUsuario>> ActualizarEquipo(int idEquipo, string nombreEquipo, bool activo);
    }

    public class EquiposRepository : IEquiposRepository
    {
        private readonly ContextData _context;

        public EquiposRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los equipos
        public async Task<IEnumerable<Equipos>> ObtenerEquipos()
        {
            return await _context.Equipos.ToListAsync();
        }

        // Método para crear un nuevo equipo
        public async Task<IEnumerable<MensajeUsuario>> CrearEquipo(Equipos equipo)
        {
            if (string.IsNullOrEmpty(equipo.NombreEquipos))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "No se puede ingresar nombres vacíos o nulos" }
                };
            }
            else
            {
                var nombreEquipoParam = new SqlParameter("@NombreEquipos", equipo.NombreEquipos);
                var activoParam = new SqlParameter("@Activo", equipo.Activo);
                var fechaRegistroParam = new SqlParameter("@Fecha_Registro", equipo.Fecha_Registro);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Equipo @NombreEquipos, @Activo, @Fecha_Registro", nombreEquipoParam, activoParam, fechaRegistroParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un equipo existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarEquipo(int idEquipo, string nombreEquipo, bool activo)
        {
            if (string.IsNullOrEmpty(nombreEquipo))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "No se puede ingresar nombres vacíos o nulos" }
                };
            }
            else
            {
                var idEquipoParam = new SqlParameter("@idEquipos", idEquipo);
                var nombreEquipoParam = new SqlParameter("@NombreEquipos", nombreEquipo);
                var activoParam = new SqlParameter("@Activo", activo);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Equipo @idEquipos, @NombreEquipos, @Activo", idEquipoParam, nombreEquipoParam, activoParam)
                    .ToListAsync();
            }
        }
    }
}
