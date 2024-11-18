using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IRolesPermisosRepository
    {
        Task<IEnumerable<RolesPermisos>> ObtenerRolesPermisos();
        Task<RolesPermisos> CrearRolPermiso(RolesPermisos rolPermiso);
    }

    public class RolesPermisosRepository : IRolesPermisosRepository
    {
        private readonly ContextData _context;
        public RolesPermisosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<RolesPermisos> CrearRolPermiso(RolesPermisos rolPermiso)
        {
            _context.RolesPermisos.Add(rolPermiso);
            await _context.SaveChangesAsync();
            return rolPermiso;
        }

        public async Task<IEnumerable<RolesPermisos>> ObtenerRolesPermisos()
        {
            return await _context.RolesPermisos
                .Include(rp => rp.Permiso)
                .Include(rp => rp.Rol)
                .ToListAsync();
        }
    }
}
