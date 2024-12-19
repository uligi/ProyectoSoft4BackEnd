using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> ObtenerRoles();
        Task<IEnumerable<MensajeUsuario>> CrearRol(Roles rol);
        Task<IEnumerable<MensajeUsuario>> ActualizarRol(int idRol, string nombreRol, bool activo, int idPermisos);
        Task<IEnumerable<MensajeUsuario>> EliminarRol(int idRol);
        Task<IEnumerable<MensajeUsuario>> ReactivarRol(int idRol);

    }

    public class RolesRepository : IRolesRepository
    {
        private readonly ContextData _context;

        public RolesRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los roles
        public async Task<IEnumerable<Roles>> ObtenerRoles()
        {
            return await _context.Roles
                .Include(r => r.Permiso) // Carga la relación con Permisos
                .ToListAsync();
        }
        public async Task<IEnumerable<MensajeUsuario>> ReactivarRol(int idRol)
        {
            var idRolParam = new SqlParameter("@idRoles", idRol);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Reactivar_Rol @idRoles", idRolParam)
                .ToListAsync();
        }

        // Método para crear un nuevo rol
        public async Task<IEnumerable<MensajeUsuario>> CrearRol(Roles rol)
        {
            if (string.IsNullOrEmpty(rol.Nombre_Roles))
            {
                return new List<MensajeUsuario>
        {
            new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del rol no puede estar vacío o nulo" }
        };
            }

            var nombreRolParam = new SqlParameter("@Nombre_Roles", rol.Nombre_Roles);
            var activoParam = new SqlParameter("@Activo", rol.Activo);
            var idPermisosParam = new SqlParameter("@idPermisos", rol.idPermisos);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Rol @Nombre_Roles, @Activo, @idPermisos",
                    nombreRolParam, activoParam, idPermisosParam)
                .ToListAsync();
        }


        // Método para actualizar un rol existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarRol(int idRol, string nombreRol, bool activo, int idPermisos)
        {
            if (string.IsNullOrEmpty(nombreRol))
            {
                return new List<MensajeUsuario>
        {
            new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del rol no puede estar vacío o nulo" }
        };
            }

            var idRolParam = new SqlParameter("@idRoles", idRol);
            var nombreRolParam = new SqlParameter("@Nombre_Roles", nombreRol);
            var activoParam = new SqlParameter("@Activo", activo);
            var idPermisosParam = new SqlParameter("@idPermisos", idPermisos);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Modificar_Rol @idRoles, @Nombre_Roles, @Activo, @idPermisos",
                            idRolParam, nombreRolParam, activoParam, idPermisosParam)
                .ToListAsync();
        }


        public async Task<IEnumerable<MensajeUsuario>> EliminarRol(int idRol)
        {
            var idRolParam = new SqlParameter("@idRoles", idRol);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Rol @idRoles", idRolParam)
                .ToListAsync();
        }

    }
}
