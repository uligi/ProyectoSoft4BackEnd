using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IMiembrosDeEquiposRepository
    {
        Task<IEnumerable<MiembrosDeEquipos>> ListarTodosLosMiembros();
        Task<IEnumerable<MensajeUsuario>> CrearMiembroEquipo(int idEquipos, int idUsuarios, bool forzar = false);

        Task<IEnumerable<MensajeUsuario>> ModificarMiembroEquipo(int idMiembrosDeEquipos, int idEquipos, int idUsuarios, bool forzar = false);

        Task<IEnumerable<MensajeUsuario>> EliminarMiembroEquipo(int idMiembrosDeEquipos);
    }

    public class MiembrosRepository : IMiembrosDeEquiposRepository
    {
        private readonly ContextData _context;

        public MiembrosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MiembrosDeEquipos>> ListarTodosLosMiembros()
        {
            return await _context.MiembrosDeEquipos
                .FromSqlRaw("EXEC Listar_Todos_Los_Miembros")
                .ToListAsync();
        }


        public async Task<IEnumerable<MensajeUsuario>> CrearMiembroEquipo(int idEquipos, int idUsuarios, bool forzar = false)
        {
            var idEquiposParam = new SqlParameter("@idEquipos", idEquipos);
            var idUsuariosParam = new SqlParameter("@idUsuarios", idUsuarios);
            var forzarParam = new SqlParameter("@forzar", forzar);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Miembro_Equipo @idEquipos, @idUsuarios, @forzar", idEquiposParam, idUsuariosParam, forzarParam)
                .ToListAsync();
        }



        public async Task<IEnumerable<MensajeUsuario>> ModificarMiembroEquipo(int idMiembrosDeEquipos, int idEquipos, int idUsuarios, bool forzar = false)
        {
            var parameters = new[]
            {
        new SqlParameter("@idMiembros_de_equipos", idMiembrosDeEquipos),
        new SqlParameter("@idEquipos", idEquipos),
        new SqlParameter("@idUsuarios", idUsuarios),
        new SqlParameter("@forzar", forzar)

    };

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Modificar_Miembro_Equipo @idMiembros_de_equipos, @idEquipos, @idUsuarios, @forzar", parameters)
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
