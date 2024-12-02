using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Controllers
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuarios>> ObtenerUsuarios();
        Task<IEnumerable<MensajeUsuario>> CrearUsuario(Usuarios usuario);
        Task<IEnumerable<MensajeUsuario>> ActualizarUsuario(int idUsuario, string nombre, string email, string contrasena, bool activo, System.DateTime fechaRegistro, int comentarioId);
    }

    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly ContextData _context;

        public UsuariosRepository(ContextData context)
        {
            _context = context;
        }

        // Método para obtener todos los usuarios
        public async Task<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Método para crear un nuevo usuario
        public async Task<IEnumerable<MensajeUsuario>> CrearUsuario(Usuarios usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.contrasena))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre, email y contraseña no pueden estar vacíos o nulos" }
                };
            }
            else
            {
                var nombreParam = new SqlParameter("@Nombre", usuario.Nombre);
                var emailParam = new SqlParameter("@Email", usuario.Email);
                var contrasenaParam = new SqlParameter("@Contrasena", usuario.contrasena);
                var activoParam = new SqlParameter("@Activo", usuario.Activo);
                var fechaRegistroParam = new SqlParameter("@FechaRegistro", usuario.FechaRegistro);
                var comentariosIdParam = new SqlParameter("@Comentarios_idComentarios", usuario.Comentarios_idComentarios);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Crear_Usuario @Nombre, @Email, @Contrasena, @Activo, @FechaRegistro, @Comentarios_idComentarios",
                                nombreParam, emailParam, contrasenaParam, activoParam, fechaRegistroParam, comentariosIdParam)
                    .ToListAsync();
            }
        }

        // Método para actualizar un usuario existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarUsuario(int idUsuario, string nombre, string email, string contrasena, bool activo, System.DateTime fechaRegistro, int comentarioId)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contrasena))
            {
                return new List<MensajeUsuario>
                {
                    new MensajeUsuario { Codigo = -3, Mensaje = "El nombre, email y contraseña no pueden estar vacíos o nulos" }
                };
            }
            else
            {
                var idUsuarioParam = new SqlParameter("@idUsuarios", idUsuario);
                var nombreParam = new SqlParameter("@Nombre", nombre);
                var emailParam = new SqlParameter("@Email", email);
                var contrasenaParam = new SqlParameter("@Contrasena", contrasena);
                var activoParam = new SqlParameter("@Activo", activo);
                var fechaRegistroParam = new SqlParameter("@FechaRegistro", fechaRegistro);
                var comentarioIdParam = new SqlParameter("@Comentarios_idComentarios", comentarioId);

                return await _context.MensajeUsuario
                    .FromSqlRaw("EXEC Modificar_Usuario @idUsuarios, @Nombre, @Email, @Contrasena, @Activo, @FechaRegistro, @Comentarios_idComentarios",
                                idUsuarioParam, nombreParam, emailParam, contrasenaParam, activoParam, fechaRegistroParam, comentarioIdParam)
                    .ToListAsync();
            }
        }
    }
}
