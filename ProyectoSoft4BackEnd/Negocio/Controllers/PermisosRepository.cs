using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IPermisosRepository
    {
        Task<IEnumerable<Permisos>> ObtenerPermisos();
        Task<IEnumerable<MensajeUsuario>> CrearPermiso(Permisos permiso);
        Task<IEnumerable<MensajeUsuario>> ActualizarPermiso(int idPermiso, string nombrePermiso, bool activo);
        Task<IEnumerable<MensajeUsuario>> EliminarPermiso(int idPermiso);

    }

    public class PermisosRepository : IPermisosRepository
    {
        private readonly ContextData _context;

        public PermisosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los permisos
        public async Task<IEnumerable<Permisos>> ObtenerPermisos()
        {
            return await _context.Permisos.ToListAsync();
        }

        // Método para crear un nuevo permiso
        public async Task<IEnumerable<MensajeUsuario>> CrearPermiso(Permisos permiso)
        {
            if (string.IsNullOrEmpty(permiso.Nombre_Permisos))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del permiso no puede estar vacío o nulo" }
                };
            }
            else
            {
                var nombrePermisoParam = new SqlParameter("@Nombre_Permisos", permiso.Nombre_Permisos);
                var activoParam = new SqlParameter("@Activo", permiso.Activo);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Permiso @Nombre_Permisos, @Activo", nombrePermisoParam, activoParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un permiso existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarPermiso(int idPermiso, string nombrePermiso, bool activo)
        {
            if (string.IsNullOrEmpty(nombrePermiso))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre del permiso no puede estar vacío o nulo" }
                };
            }
            else
            {
                var idPermisoParam = new SqlParameter("@idPermisos", idPermiso);
                var nombrePermisoParam = new SqlParameter("@Nombre_Permisos", nombrePermiso);
                var activoParam = new SqlParameter("@Activo", activo);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Permiso @idPermisos, @Nombre_Permisos, @Activo", idPermisoParam, nombrePermisoParam, activoParam)
                    .ToListAsync();
            }
        }
        public async Task<IEnumerable<MensajeUsuario>> EliminarPermiso(int idPermiso)
        {
            var idPermisoParam = new SqlParameter("@idPermisos", idPermiso);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Permiso @idPermisos", idPermisoParam)
                .ToListAsync();
        }

    }
}
