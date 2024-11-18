using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IHistorialDeCambiosRepository
    {
        Task<IEnumerable<HistorialDeCambios>> ObtenerHistorialDeCambios();
        Task<HistorialDeCambios> CrearHistorialDeCambio(HistorialDeCambios historial);
    }

    public class HistorialDeCambiosRepository : IHistorialDeCambiosRepository
    {
        private readonly ContextData _context;
        public HistorialDeCambiosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<HistorialDeCambios> CrearHistorialDeCambio(HistorialDeCambios historial)
        {
            _context.HistorialDeCambios.Add(historial);
            await _context.SaveChangesAsync();
            return historial;
        }

        public async Task<IEnumerable<HistorialDeCambios>> ObtenerHistorialDeCambios()
        {
            return await _context.HistorialDeCambios
                .Include(h => h.Tarea)
                .Include(h => h.Proyecto)
                .Include(h => h.Portafolio)
                .ToListAsync();
        }
    }
}
