using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IComentariosRepository
    {
        Task<IEnumerable<Comentarios>> ObtenerComentarios(string textoComentario);
        Task<IEnumerable<MensajeUsuario>> CrearComentario(Comentarios comentario);
        Task<IEnumerable<MensajeUsuario>> ActualizarComentario(int idComentario, string textoComentario, bool activo);
    }

    public class ComentariosRepository : IComentariosRepository
    {
        private readonly ContextData _context; // Corregido el nombre del contexto

        public ComentariosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener comentarios filtrados
        public async Task<IEnumerable<Comentarios>> ObtenerComentarios(string textoComentario)
        {
            return await _context.Comentarios
                .Where(c => !string.IsNullOrEmpty(textoComentario) ? c.Comentario.Contains(textoComentario) : true)
                .ToListAsync();
        }

        // Método para crear un nuevo comentario
        public async Task<IEnumerable<MensajeUsuario>> CrearComentario(Comentarios comentario)
        {
            if (string.IsNullOrEmpty(comentario.Comentario))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "No se puede ingresar comentarios vacíos o nulos" }
                };
            }
            else
            {
                var comentarioParam = new SqlParameter("@Comentario", comentario.Comentario);
                var fechaCreacionParam = new SqlParameter("@FechaCreacion", comentario.FechaCreacion);
                var activoParam = new SqlParameter("@Activo", comentario.Activo);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Comentario @Comentario, @FechaCreacion, @Activo", comentarioParam, fechaCreacionParam, activoParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un comentario existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarComentario(int idComentario, string textoComentario, bool activo)
        {
            if (string.IsNullOrEmpty(textoComentario))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "No se puede ingresar comentarios vacíos o nulos" }
                };
            }
            else
            {
                var idComentarioParam = new SqlParameter("@idComentarios", idComentario);
                var comentarioParam = new SqlParameter("@Comentario", textoComentario);
                var activoParam = new SqlParameter("@Activo", activo);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Comentario @idComentarios, @Comentario, @Activo", idComentarioParam, comentarioParam, activoParam)
                    .ToListAsync();
            }
        }
    }
}
