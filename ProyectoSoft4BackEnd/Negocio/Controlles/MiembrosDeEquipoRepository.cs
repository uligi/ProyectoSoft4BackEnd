using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IMiembrosDeEquiposRepository
    {
        Task<IEnumerable<MiembrosDeEquipos>> ObtenerMiembrosDeEquipos();
        Task<MiembrosDeEquipos> CrearMiembroDeEquipo(MiembrosDeEquipos miembro);
    }

    public class MiembrosDeEquiposRepository : IMiembrosDeEquiposRepository
    {
        private readonly ContextData _context;
        public MiembrosDeEquiposRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<MiembrosDeEquipos> CrearMiembroDeEquipo(MiembrosDeEquipos miembro)
        {
            _context.MiembrosDeEquipos.Add(miembro);
            await _context.SaveChangesAsync();
            return miembro;
        }

        public async Task<IEnumerable<MiembrosDeEquipos>> ObtenerMiembrosDeEquipos()
        {
            return await _context.MiembrosDeEquipos
                .Include(m => m.Equipo)
                .Include(m => m.Usuario)
                .Include(m => m.RolPermiso)
                .ToListAsync();
        }
    }
}
