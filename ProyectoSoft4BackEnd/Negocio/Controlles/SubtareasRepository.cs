using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface ISubtareasRepository
    {
        Task<IEnumerable<Subtareas>> ObtenerSubtareas();
        Task<Subtareas> CrearSubtarea(Subtareas subtarea);
    }

    public class SubtareasRepository : ISubtareasRepository
    {
        private readonly ContextData _context;
        public SubtareasRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Subtareas> CrearSubtarea(Subtareas subtarea)
        {
            _context.Subtareas.Add(subtarea);
            await _context.SaveChangesAsync();
            return subtarea;
        }

        public async Task<IEnumerable<Subtareas>> ObtenerSubtareas()
        {
            return await _context.Subtareas.ToListAsync();
        }
    }
}
