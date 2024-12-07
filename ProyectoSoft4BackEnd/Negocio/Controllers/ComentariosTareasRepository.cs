using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio.Controllers
{
   
        public interface IComentariosTareasRepository
        {
            Task<IEnumerable<ComentariosTareasResponse>> ListarComentarios();
            Task<int> AgregarComentario(ComentariosTareasRequest comentario);
            Task<string> ActualizarComentario(ComentariosTareasRequest comentario);
            Task<string> EliminarComentario(int idComentario);
       }
    
        public class ComentariosTareasRepository : IComentariosTareasRepository
        {
            private readonly ContextData _context;

            public ComentariosTareasRepository(ContextData context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ComentariosTareasResponse>> ListarComentarios()
            {
                var comentarios = await _context.ComentariosTareas
                    .FromSqlRaw("EXEC Listar_Comentarios_Tareas")
                    .ToListAsync();

                return comentarios.Select(c => new ComentariosTareasResponse
                {
                    idComentario = c.idComentario,
                    Comentario = c.Comentario,
                    FechaCreacion = c.FechaCreacion,
                    Activo = c.Activo,
                    NombreTarea = c.NombreTarea,
                    NombreUsuario = c.NombreUsuario
                });
            }

            public async Task<int> AgregarComentario(ComentariosTareasRequest comentario)
            {
                var parameters = new[]
                {
                new SqlParameter("@Comentario", comentario.Comentario),
                new SqlParameter("@FechaCreacion", comentario.FechaCreacion),
                new SqlParameter("@Activo", comentario.Activo),
                new SqlParameter("@idTarea", comentario.idTarea),
                new SqlParameter("@idUsuario", comentario.idUsuario)
            };

                var result = await _context.ComentariosTareas
                    .FromSqlRaw("EXEC Agregar_Comentario_Tarea @Comentario, @FechaCreacion, @Activo, @idTarea, @idUsuario", parameters)
                    .ToListAsync();

                return result.FirstOrDefault()?.idComentario ?? 0;
            }

            public async Task<string> ActualizarComentario(ComentariosTareasRequest comentario)
            {
                var parameters = new[]
                {
                new SqlParameter("@idComentario", comentario.idComentario),
                new SqlParameter("@Comentario", comentario.Comentario),
                new SqlParameter("@Activo", comentario.Activo)
            };

                var result = await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Actualizar_Comentario_Tarea @idComentario, @Comentario, @Activo", parameters)
                    .FirstOrDefaultAsync();

                return result?.Mensaje ?? "Error al actualizar el comentario.";
            }

            public async Task<string> EliminarComentario(int idComentario)
            {
                var parameter = new SqlParameter("@idComentario", idComentario);

                var result = await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Eliminar_Comentario_Tarea @idComentario", parameter)
                    .FirstOrDefaultAsync();

                return result?.Mensaje ?? "Error al eliminar el comentario.";
            }
        }
    }

