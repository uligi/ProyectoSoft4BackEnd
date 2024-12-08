using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Negocio.Controllers
{
    public interface IComentariosProyectosRepository
    {
        Task<IEnumerable<ComentariosProyectosResponse>> ListarComentarios();
        Task<int> AgregarComentario(ComentariosProyectosRequest comentario);
        Task<string> ActualizarComentario(ComentariosProyectosRequest comentario);
        Task<string> EliminarComentario(int idComentario);
    }


    public class ComentariosProyectosRepository : IComentariosProyectosRepository
    {
        private readonly ContextData _context;

        public ComentariosProyectosRepository(ContextData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComentariosProyectosResponse>> ListarComentarios()
        {
            try
            {
                var comentarios = await _context.ComentariosProyectos
                    .FromSqlRaw("EXEC Listar_Comentarios_Proyectos")
                    .ToListAsync();

                // Log para inspección
                Console.WriteLine($"Comentarios obtenidos: {JsonConvert.SerializeObject(comentarios)}");

                return comentarios.Select(c => new ComentariosProyectosResponse
                {
                    idComentario = c.idComentario,
                    Comentario = c.Comentario,
                    FechaCreacion = c.FechaCreacion,
                    Activo = c.Activo,
                    idProyecto = c.idProyecto,
                    NombreProyecto = c.NombreProyecto,
                    idUsuario = c.idUsuario,
                    NombreUsuario = c.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        public async Task<int> AgregarComentario(ComentariosProyectosRequest comentario)
        {
            var parameters = new[]
            {
            new SqlParameter("@Comentario", comentario.Comentario),
            new SqlParameter("@FechaCreacion", comentario.FechaCreacion),
            new SqlParameter("@idProyecto", comentario.idProyecto),
            new SqlParameter("@idUsuario", comentario.idUsuario)
        };

            var result = await _context.ComentariosProyectos
                .FromSqlRaw("EXEC Agregar_Comentario_Proyectos @Comentario, @FechaCreacion, @idProyecto, @idUsuario", parameters)
                .ToListAsync();

            return result.FirstOrDefault()?.idComentario ?? 0;
        }

        public async Task<string> ActualizarComentario(ComentariosProyectosRequest comentario)
        {
            var parameters = new[]
            {
            new SqlParameter("@idComentario", comentario.idComentario),
            new SqlParameter("@Comentario", comentario.Comentario),
          
        };

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Actualizar_Comentario_Proyectos @idComentario, @Comentario", parameters)
                .FirstOrDefaultAsync();

            return result?.Mensaje ?? "Error al actualizar el comentario.";
        }

        public async Task<string> EliminarComentario(int idComentario)
        {
            var parameter = new SqlParameter("@idComentario", idComentario);

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Comentario_Proyectos @idComentario", parameter)
                .FirstOrDefaultAsync();

            return result?.Mensaje ?? "Error al eliminar el comentario.";
        }

    }
}
