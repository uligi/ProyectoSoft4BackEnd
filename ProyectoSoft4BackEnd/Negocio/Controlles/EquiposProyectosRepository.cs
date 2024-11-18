using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IEquiposProyectosRepository
    {
        Task<IEnumerable<EquiposProyectos>> ObtenerEquiposProyectos();
        Task<EquiposProyectos> CrearEquipoProyecto(EquiposProyectos equipoProyecto);
    }

    public class EquiposProyectosRepository : IEquiposProyectosRepository
    {
        private readonly ContextData _context;
        public EquiposProyectosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<EquiposProyectos> CrearEquipoProyecto(EquiposProyectos equipoProyecto)
        {
            _context.EquiposProyectos.Add(equipoProyecto);
            await _context.SaveChangesAsync();
            return equipoProyecto;
        }

        public async Task<IEnumerable<EquiposProyectos>> ObtenerEquiposProyectos()
        {
            return await _context.EquiposProyectos
                .Include(ep => ep.Equipo)
                .Include(ep => ep.Proyecto)
                .ToListAsync();
        }
    }
}
