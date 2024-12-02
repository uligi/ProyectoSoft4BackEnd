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
        Task<IEnumerable<Miembros_de_equipos>> ObtenerMiembrosDeEquipos();
        Task<IEnumerable<MensajeUsuario>> CrearMiembroDeEquipo(Miembros_de_equipos miembro);
        Task<IEnumerable<MensajeUsuario>> ActualizarMiembroDeEquipo(int idMiembro, int equiposId, int usuariosId, int rolesPermisosId);
    }

    public class MiembrosDeEquiposRepository : IMiembrosDeEquiposRepository
    {
        private readonly ContextData _context;

        public MiembrosDeEquiposRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los miembros de los equipos
        public async Task<IEnumerable<Miembros_de_equipos>> ObtenerMiembrosDeEquipos()
        {
            return await _context.Miembros_de_equipos.ToListAsync();
        }

        // Método para crear un nuevo miembro de equipo
        public async Task<IEnumerable<MensajeUsuario>> CrearMiembroDeEquipo(Miembros_de_equipos miembro)
        {
            if (miembro.Equipos_idEquipos <= 0 || miembro.Usuarios_idUsuarios <= 0 || miembro.RolesPermisos_idRolesPermisos <= 0)
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "Los identificadores de equipo, usuario o roles deben ser válidos" }
                };
            }
            else
            {
                var equiposIdParam = new SqlParameter("@Equipos_idEquipos", miembro.Equipos_idEquipos);
                var usuariosIdParam = new SqlParameter("@Usuarios_idUsuarios", miembro.Usuarios_idUsuarios);
                var rolesPermisosIdParam = new SqlParameter("@RolesPermisos_idRolesPermisos", miembro.RolesPermisos_idRolesPermisos);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Miembro_Equipo @Equipos_idEquipos, @Usuarios_idUsuarios, @RolesPermisos_idRolesPermisos",
                                equiposIdParam, usuariosIdParam, rolesPermisosIdParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un miembro de equipo existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarMiembroDeEquipo(int idMiembro, int equiposId, int usuariosId, int rolesPermisosId)
        {
            if (equiposId <= 0 || usuariosId <= 0 || rolesPermisosId <= 0)
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "Los identificadores de equipo, usuario o roles deben ser válidos" }
                };
            }
            else
            {
                var idMiembroParam = new SqlParameter("@idMiembros_de_equipos", idMiembro);
                var equiposIdParam = new SqlParameter("@Equipos_idEquipos", equiposId);
                var usuariosIdParam = new SqlParameter("@Usuarios_idUsuarios", usuariosId);
                var rolesPermisosIdParam = new SqlParameter("@RolesPermisos_idRolesPermisos", rolesPermisosId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Miembro_Equipo @idMiembros_de_equipos, @Equipos_idEquipos, @Usuarios_idUsuarios, @RolesPermisos_idRolesPermisos",
                                idMiembroParam, equiposIdParam, usuariosIdParam, rolesPermisosIdParam)
                    .ToListAsync();
            }
        }
    }
}
