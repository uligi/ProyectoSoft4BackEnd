using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IRolesPermisosRepository
    {
        Task<IEnumerable<RolesPermisos>> ObtenerRolesPermisos();
        Task<IEnumerable<MensajeUsuario>> CrearRolPermiso(RolesPermisos rolesPermisos);
        Task<IEnumerable<MensajeUsuario>> ActualizarRolPermiso(int idRolPermiso, int permisosId, int rolesId);
    }

    public class RolesPermisosRepository : IRolesPermisosRepository
    {
        private readonly ContextData _context;

        public RolesPermisosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los roles y permisos
        public async Task<IEnumerable<RolesPermisos>> ObtenerRolesPermisos()
        {
            return await _context.RolesPermisos.ToListAsync();
        }

        // Método para crear una nueva relación entre roles y permisos
        public async Task<IEnumerable<MensajeUsuario>> CrearRolPermiso(RolesPermisos rolesPermisos)
        {
            if (rolesPermisos.Permisos_idPermisos <= 0 || rolesPermisos.Roles_idRoles <= 0)
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "Los identificadores de permisos y roles deben ser válidos" }
                };
            }
            else
            {
                var permisosIdParam = new SqlParameter("@Permisos_idPermisos", rolesPermisos.Permisos_idPermisos);
                var rolesIdParam = new SqlParameter("@Roles_idRoles", rolesPermisos.Roles_idRoles);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Rol_Permiso @Permisos_idPermisos, @Roles_idRoles", permisosIdParam, rolesIdParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar una relación entre roles y permisos
        public async Task<IEnumerable<MensajeUsuario>> ActualizarRolPermiso(int idRolPermiso, int permisosId, int rolesId)
        {
            if (permisosId <= 0 || rolesId <= 0)
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "Los identificadores de permisos y roles deben ser válidos" }
                };
            }
            else
            {
                var idRolPermisoParam = new SqlParameter("@idRolesPermisos", idRolPermiso);
                var permisosIdParam = new SqlParameter("@Permisos_idPermisos", permisosId);
                var rolesIdParam = new SqlParameter("@Roles_idRoles", rolesId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Rol_Permiso @idRolesPermisos, @Permisos_idPermisos, @Roles_idRoles",
                                idRolPermisoParam, permisosIdParam, rolesIdParam)
                    .ToListAsync();
            }
        }
    }
}
