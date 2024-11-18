using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace Negocio.Controllers
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> ObtenerRoles();
        Task<Roles> CrearRol(Roles rol);
    }

    public class RolesRepository : IRolesRepository
    {
        private readonly ContextData _context;
        public RolesRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Roles> CrearRol(Roles rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<IEnumerable<Roles>> ObtenerRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
