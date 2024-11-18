using Negocio.Data;
using Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuarios>> ObtenerUsuarios();
        Task<Usuarios> CrearUsuario(Usuarios usuario);
    }

    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly ContextData _context;
        public UsuariosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<Usuarios> CrearUsuario(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.Comentario)
                .ToListAsync();
        }
    }
}
