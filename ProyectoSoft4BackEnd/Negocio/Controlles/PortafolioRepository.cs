using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IPortafolioRepository
    {
        Task<IEnumerable<Portafolio>> ObtenerPortafolios();
        Task<Portafolio> CrearPortafolio(Portafolio portafolio);
    }

    public class PortafolioRepository : IPortafolioRepository
    {
        private readonly ContextData _context;
        public PortafolioRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Portafolio> CrearPortafolio(Portafolio portafolio)
        {
            _context.Portafolio.Add(portafolio);
            await _context.SaveChangesAsync();
            return portafolio;
        }

        public async Task<IEnumerable<Portafolio>> ObtenerPortafolios()
        {
            return await _context.Portafolio.ToListAsync();
        }
    }
}
