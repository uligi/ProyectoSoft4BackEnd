using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Negocio.Controllers.ComentariosSubtareasRepository;



namespace Negocio.Controllers
{
 
    
        public interface IComentariosSubtareasRepository
        {
            Task<IEnumerable<ComentariosSubtareasResponse>> ListarComentarios();
            Task<int> AgregarComentario(ComentariosSubtareasRequest comentario);
            Task<string> ActualizarComentario(ComentariosSubtareasRequest comentario);
            Task<string> EliminarComentario(int idComentario);
        }
       
    public class ComentariosSubtareasRepository : IComentariosSubtareasRepository
    {
        private readonly ContextData _context;
        public ComentariosSubtareasRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComentariosSubtareasResponse>> ListarComentarios()
        {
            var comentarios = await _context.ComentariosSubtareas
                .FromSqlRaw("EXEC Listar_Comentarios_Subtareas")
                .ToListAsync();

            return comentarios.Select(c => new ComentariosSubtareasResponse
            {
                idComentario = c.idComentario,
                Comentario = c.Comentario,
                FechaCreacion = c.FechaCreacion,
                Activo = c.Activo,
                NombreSubtarea = c.NombreSubtarea,
                NombreUsuario = c.NombreUsuario
            });
        }

        public async Task<int> AgregarComentario(ComentariosSubtareasRequest comentario)
        {
            var parameters = new[]
            {
                new SqlParameter("@Comentario", comentario.Comentario),
                new SqlParameter("@FechaCreacion", comentario.FechaCreacion),
                new SqlParameter("@Activo", comentario.Activo),
                new SqlParameter("@idSubtarea", comentario.idSubtarea),
                new SqlParameter("@idUsuario", comentario.idUsuario)
            };

            var result = await _context.ComentariosSubtareas
                .FromSqlRaw("EXEC Agregar_Comentario_Subtarea @Comentario, @FechaCreacion, @Activo, @idSubtarea, @idUsuario", parameters)
                .ToListAsync();

            return result.FirstOrDefault()?.idComentario ?? 0;
        }

        public async Task<string> ActualizarComentario(ComentariosSubtareasRequest comentario)
        {
            var parameters = new[]
            {
                new SqlParameter("@idComentario", comentario.idComentario),
                new SqlParameter("@Comentario", comentario.Comentario),
                new SqlParameter("@Activo", comentario.Activo)
            };

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Comentario_Subtarea @idComentario, @Comentario, @Activo", parameters)
                .FirstOrDefaultAsync();

            return result?.Mensaje ?? "Error al actualizar el comentario.";
        }

        public async Task<string> EliminarComentario(int idComentario)
        {
            var parameter = new SqlParameter("@idComentario", idComentario);

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Comentario_Subtarea @idComentario", parameter)
                .FirstOrDefaultAsync();

            return result?.Mensaje ?? "Error al eliminar el comentario.";
        }
    }
}
