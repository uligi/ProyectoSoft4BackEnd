using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IMiembrosDeEquiposRepository
    {
        Task<IEnumerable<MiembrosDeEquipos>> ListarMiembrosEquipo(int idEquipos);
        Task<IEnumerable<MensajeUsuario>> CrearMiembroEquipo(int idEquipos, int idUsuarios);
        Task<IEnumerable<MensajeUsuario>> EliminarMiembroEquipo(int idMiembrosDeEquipos);
    }

    public class MiembrosRepository : IMiembrosDeEquiposRepository
    {
        private readonly ContextData _context;

        public MiembrosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MiembrosDeEquipos>> ListarMiembrosEquipo(int idEquipos)
        {
            var idEquiposParam = new SqlParameter("@idEquipos", idEquipos);

            return await _context.MiembrosDeEquipos
                .FromSqlRaw("EXEC Listar_Miembros_Equipo @idEquipos", idEquiposParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> CrearMiembroEquipo(int idEquipos, int idUsuarios)
        {
            var idEquiposParam = new SqlParameter("@idEquipos", idEquipos);
            var idUsuariosParam = new SqlParameter("@idUsuarios", idUsuarios);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Miembro_Equipo @idEquipos, @idUsuarios", idEquiposParam, idUsuariosParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> EliminarMiembroEquipo(int idMiembrosDeEquipos)
        {
            var idMiembrosDeEquiposParam = new SqlParameter("@idMiembros_de_equipos", idMiembrosDeEquipos);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Miembro_Equipo @idMiembros_de_equipos", idMiembrosDeEquiposParam)
                .ToListAsync();
        }
    }
}
