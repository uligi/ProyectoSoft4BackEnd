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
        Task<IEnumerable<ComentariosTareasResponse>> ListarComentariosPorTarea(int idTarea);
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
                new SqlParameter("@idTarea", comentario.idTarea),
                new SqlParameter("@idUsuario", comentario.idUsuario)
            };

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC Agregar_Comentario_Tarea @Comentario, @FechaCreacion, @idTarea, @idUsuario",
                parameters
            );

            return result;
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
                .ToListAsync();

            var response = result.FirstOrDefault();
            return response?.Codigo == 1 ? response.Mensaje : "Error al actualizar el comentario.";
        }


        public async Task<string> EliminarComentario(int idComentario)
        {
            var parameter = new SqlParameter("@idComentario", idComentario);

            var result = await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Comentario_Tarea @idComentario", parameter)
                .ToListAsync();

            var response = result.FirstOrDefault();
            return response?.Codigo == 1 ? response.Mensaje : "Error al eliminar el comentario.";
        }


        // **********************************************************Por Tarea********************************************


        public async Task<IEnumerable<ComentariosTareasResponse>> ListarComentariosPorTarea(int idTarea)
        {
            try
            {
                var parameter = new SqlParameter("@idTarea", idTarea);

                var comentarios = await _context.ComentariosTareas
                    .FromSqlRaw("EXEC Listar_Comentarios_Por_Tarea @idTarea", parameter)
                    .ToListAsync();

                return comentarios.Select(c => new ComentariosTareasResponse
                {
                    idComentario = c.idComentario,
                    Comentario = c.Comentario,
                    FechaCreacion = c.FechaCreacion,
                    Activo = c.Activo,
                    idTareas = c.idTareas,
                    NombreTarea = c.NombreTarea,
                    idUsuario = c.idUsuario,
                    NombreUsuario = c.NombreUsuario
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar comentarios: {ex.Message}");
                throw;
            }
        }

    }
}

