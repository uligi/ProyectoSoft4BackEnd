using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IComentariosRepository
    {
        Task<IEnumerable<Comentarios>> ObtenerComentarios();
        Task<Comentarios> CrearComentario(Comentarios comentario);
    }

    public class ComentariosRepository : IComentariosRepository
    {
        private readonly ContextData _context;
        public ComentariosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Comentarios> CrearComentario(Comentarios comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }

        public async Task<IEnumerable<Comentarios>> ObtenerComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }
    }
}
