using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IPermisosRepository
    {
        Task<IEnumerable<Permisos>> ObtenerPermisos();
        Task<Permisos> CrearPermiso(Permisos permiso);
    }

    public class PermisosRepository : IPermisosRepository
    {
        private readonly ContextData _context;
        public PermisosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Permisos> CrearPermiso(Permisos permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
            return permiso;
        }

        public async Task<IEnumerable<Permisos>> ObtenerPermisos()
        {
            return await _context.Permisos.ToListAsync();
        }
    }
}
