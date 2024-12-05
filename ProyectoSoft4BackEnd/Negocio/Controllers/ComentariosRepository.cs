using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IComentariosRepository
    {
        Task<IEnumerable<ComentariosResponse>> ListarComentarios();
        Task<int> AgregarComentario(ComentariosRequest comentario);
        Task<string> ActualizarComentario(ComentariosRequest comentario);
        Task<string> EliminarComentario(int idComentarios);
    }

    public class ComentariosRepository : IComentariosRepository
    {
        private readonly ContextData _context;

        public ComentariosRepository(ContextData context)
        {
            _context = context;
        }

   public async Task<IEnumerable<ComentariosResponse>> ListarComentarios()
{
    var comentarios = await _context.Comentarios
        .FromSqlRaw("EXEC Listar_Comentarios")
        .ToListAsync();

    return comentarios.Select(c => new ComentariosResponse
    {
        idComentarios = c.idComentarios,
        Comentario = c.Comentario,
        FechaCreacion = c.FechaCreacion,
        Activo = c.Activo,
        Tareas_idTareas = c.Tareas_idTareas,
        idSubtareas = c.idSubtareas,
        idProyectos = c.idProyectos
    });
}


        public async Task<int> AgregarComentario(ComentariosRequest comentario)
        {
            var parameters = new[]
            {
                new SqlParameter("@Comentario", comentario.Comentario),
                new SqlParameter("@FechaCreacion", comentario.FechaCreacion),
                
                new SqlParameter("@Tareas_idTareas", comentario.Tareas_idTareas ?? (object)DBNull.Value),
                new SqlParameter("@idSubtareas", comentario.idSubtareas ?? (object)DBNull.Value),
                new SqlParameter("@idProyectos", comentario.idProyectos ?? (object)DBNull.Value)
            };

            var result = await _context.Comentarios
                .FromSqlRaw("EXEC Agregar_Comentario @Comentario, @FechaCreacion, @Tareas_idTareas, @idSubtareas, @idProyectos", parameters)
                .ToListAsync();

            // Suponiendo que el procedimiento devuelve el ID del comentario agregado
            return result.FirstOrDefault()?.idComentarios ?? 0;
        }

        public async Task<string> ActualizarComentario(ComentariosRequest comentario)
        {
            var parameters = new[]
            {
                new SqlParameter("@idComentarios", comentario.idComentarios),
                new SqlParameter("@Comentario", comentario.Comentario),
                
            };

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Comentario @idComentarios, @Comentario", parameters)
                .FirstOrDefaultAsync();

            // Acceder al mensaje dentro del objeto
            return result?.Mensaje ?? "Error al actualizar el comentario.";
        }

        public async Task<string> EliminarComentario(int idComentarios)
        {
            var parameter = new SqlParameter("@idComentarios", idComentarios);

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Comentario @idComentarios", parameter)
                .FirstOrDefaultAsync();

            // Acceder al mensaje dentro del objeto
            return result?.Mensaje ?? "Error al eliminar el comentario.";
        }
    }
}
