using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    using global::Negocio.Controllers.Negocio.Controllers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace Negocio.Controllers
    {
        public interface IEquiposRepository
        {
            Task<IEnumerable<Equipos>> ObtenerEquipos();
            Task<IEnumerable<Equipos>> ObtenerEquiposActivos();
            Task<IEnumerable<MensajeUsuario>> CrearEquipo(Equipos equipo);
            Task<IEnumerable<MensajeUsuario>> ActualizarEquipo(int idEquipos, string nombreEquipos);
            Task<IEnumerable<MensajeUsuario>> EliminarEquipo(int idEquipos);

            Task<IEnumerable<MensajeUsuario>> ReactivarEquipos(int idEquipos);
        }
    }


    
        public class EquiposRepository : IEquiposRepository
        {
            private readonly ContextData _context;

            public EquiposRepository(ContextData context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Equipos>> ObtenerEquiposActivos()
            {
                return await _context.Equipos.Where(e => e.Activo).ToListAsync();
            }
        public async Task<IEnumerable<Equipos>> ObtenerEquipos()
        {
            return await _context.Equipos.ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> ReactivarEquipos(int idEquipos)
        {
            var idParam = new SqlParameter("@idEquipos", idEquipos);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Reactivar_Equipos @idEquipos", idParam)
                .ToListAsync();
        }
        public async Task<IEnumerable<MensajeUsuario>> CrearEquipo(Equipos equipo)
        {
            var nombreParam = new SqlParameter("@NombreEquipos", equipo.NombreEquipos);
            var activoParam = new SqlParameter("@Activo", equipo.Activo);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Equipo @NombreEquipos, @Activo", nombreParam, activoParam)
                .ToListAsync();
        }


        public async Task<IEnumerable<MensajeUsuario>> ActualizarEquipo(int idEquipos, string nombreEquipos)
            {
                var idParam = new SqlParameter("@idEquipos", idEquipos);
                var nombreParam = new SqlParameter("@NombreEquipos", nombreEquipos);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Actualizar_Equipo @idEquipos, @NombreEquipos", idParam, nombreParam)
                    .ToListAsync();
            }

            public async Task<IEnumerable<MensajeUsuario>> EliminarEquipo(int idEquipos)
            {
                var idParam = new SqlParameter("@idEquipos", idEquipos);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Eliminar_Equipo @idEquipos", idParam)
                    .ToListAsync();
            }
        }
}
