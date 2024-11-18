using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Negocio.Controllers
{
    public interface ITareasRepository
    {
        Task<IEnumerable<Tareas>> ObtenerTareas();
        Task<Tareas> CrearTarea(Tareas tarea);
    }

    public class TareasRepository : ITareasRepository
    {
        private readonly ContextData _context;
        public TareasRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Tareas> CrearTarea(Tareas tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<IEnumerable<Tareas>> ObtenerTareas()
        {
            return await _context.Tareas
                .Include(t => t.Subtarea)
                .Include(t => t.Proyecto)
                .Include(t => t.Comentario)
                .ToListAsync();
        }
    }
}
