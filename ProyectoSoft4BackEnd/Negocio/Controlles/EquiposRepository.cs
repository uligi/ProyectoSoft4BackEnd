using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IEquiposRepository
    {
        Task<IEnumerable<Equipos>> ObtenerEquipos();
        Task<Equipos> CrearEquipo(Equipos equipo);
    }

    public class EquiposRepository : IEquiposRepository
    {
        private readonly ContextData _context;
        public EquiposRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Equipos> CrearEquipo(Equipos equipo)
        {
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();
            return equipo;
        }

        public async Task<IEnumerable<Equipos>> ObtenerEquipos()
        {
            return await _context.Equipos.ToListAsync();
        }
    }
}
