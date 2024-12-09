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
        Task<IEnumerable<ComentariosProyectosResponse>> ListarComentariosPorProyecto(int idProyecto);

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
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@Comentario", comentario.Comentario),
            new SqlParameter("@FechaCreacion", comentario.FechaCreacion),
            new SqlParameter("@idProyecto", comentario.idProyecto),
            new SqlParameter("@idUsuario", comentario.idUsuario)
        };

                // Ejecuta el procedimiento y obtiene el ID del nuevo comentario
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC Agregar_Comentario_Proyectos @Comentario, @FechaCreacion, @idProyecto, @idUsuario",
                    parameters
                );

                return Convert.ToInt32(result); // Devuelve el ID como entero
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el comentario: {ex.Message}");
                throw;
            }
        }


        public async Task<string> ActualizarComentario(ComentariosProyectosRequest comentario)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@idComentario", comentario.idComentario),
            new SqlParameter("@Comentario", comentario.Comentario)
        };

                var result = await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Actualizar_Comentario_Proyectos @idComentario, @Comentario", parameters)
                    .ToListAsync();

                var response = result.FirstOrDefault();
                if (response != null && response.Codigo == 1) // Verifica Codigo
                {
                    return response.Mensaje;
                }

                return "Error al actualizar el comentario.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<string> EliminarComentario(int idComentario)
        {
            try
            {
                var parameter = new SqlParameter("@idComentario", idComentario);

                var result = await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Eliminar_Comentario_Proyectos @idComentario", parameter)
                    .ToListAsync();

                var response = result.FirstOrDefault();
                if (response != null && response.Codigo == 1) // Verifica Codigo
                {
                    return response.Mensaje;
                }

                return "Error al eliminar el comentario.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el comentario: {ex.Message}");
                throw;
            }
        }

        // **********************************************************Por proyecto********************************************


        public async Task<IEnumerable<ComentariosProyectosResponse>> ListarComentariosPorProyecto(int idProyecto)
        {
            try
            {
                var parameter = new SqlParameter("@idProyecto", idProyecto);

                var comentarios = await _context.ComentariosProyectos
                    .FromSqlRaw("EXEC Listar_Comentarios_Por_Proyecto @idProyecto", parameter)
                    .ToListAsync();

                return comentarios.Select(c => new ComentariosProyectosResponse
                {
                    idComentario = c.idComentario,
                    Comentario = c.Comentario,
                    FechaCreacion = c.FechaCreacion,
                    Activo = c.Activo,
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
