﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Negocio.Data;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Negocio.Controllers;
using Negocio.Repositories;

namespace Negocio.Controllers
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuarios>> ObtenerUsuarios();
        Task<IEnumerable<MensajeUsuario>> CrearUsuario(Usuarios usuario);
        Task<IEnumerable<MensajeUsuario>> ActualizarUsuario(int idUsuario, string nombre, string email, int idRoles);
        Task<IEnumerable<MensajeUsuario>> EliminarUsuario(int idUsuario);
        Task<IEnumerable<MensajeUsuario>> AsignarUsuarioAEquipo(int idUsuario, int idEquipo);
        Task<IEnumerable<MensajeUsuario>> ReactivarUsuario(int idUsuario);

        Task<string> RestablecerContrasena(int idUsuario);
        Task<Usuarios> ObtenerUsuariosPorId(int idUsuario);
        Task<IEnumerable<Usuarios>> ObtenerUsuariosActivos();



    }


    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly ContextData _context;
        private readonly IRecursosRepository _recursos;

        public UsuariosRepository(ContextData context, IRecursosRepository recursos)
        {
            _context = context;
            _recursos = recursos;
        }

        // Método para obtener todos los usuarios
        public async Task<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.Rol) // Incluye los datos de la tabla Roles
                .ThenInclude(r => r.Permiso) // Incluye los datos de la tabla Permisos
                .ToListAsync();
        }

        public async Task<Usuarios> ObtenerUsuariosPorId(int idUsuario)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol) // Incluye los datos del rol relacionado
                .FirstOrDefaultAsync(u => u.idUsuarios == idUsuario);

            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            return usuario;
        }
        public async Task<IEnumerable<Usuarios>> ObtenerUsuariosActivos()
        {
            return await _context.Usuarios
                .FromSqlRaw("EXEC [dbo].[sp_Listar_Usuarios_Activos]")
                .ToListAsync();
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

            var nombreParam = new SqlParameter("@Nombre", usuario.Nombre);
            var emailParam = new SqlParameter("@Email", usuario.Email);
            var contrasenaParam = new SqlParameter("@Contrasena", usuario.contrasena);
            var idRolesParam = new SqlParameter("@idRoles", usuario.idRoles);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Crear_Usuario @Nombre, @Email, @Contrasena, @idRoles",
                            nombreParam, emailParam, contrasenaParam, idRolesParam)
                .ToListAsync();
        }




        // Método para actualizar un usuario existente
        public async Task<IEnumerable<MensajeUsuario>> ActualizarUsuario(int idUsuario, string nombre, string email, int idRoles)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email))
            {
                return new List<MensajeUsuario>
        {
            new MensajeUsuario { Codigo = -3, Mensaje = "El nombre y el email no pueden estar vacíos o nulos." }
        };
            }

            var idUsuarioParam = new SqlParameter("@idUsuarios", idUsuario);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var emailParam = new SqlParameter("@Email", email);
            var idRolesParam = new SqlParameter("@idRoles", idRoles);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Modificar_Usuario @idUsuarios, @Nombre, @Email, @idRoles",
                    idUsuarioParam, nombreParam, emailParam, idRolesParam)
                .ToListAsync();
        }




        public async Task<IEnumerable<MensajeUsuario>> EliminarUsuario(int idUsuario)
        {
            var idUsuarioParam = new SqlParameter("@idUsuarios", idUsuario);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Eliminar_Usuario @idUsuarios", idUsuarioParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<MensajeUsuario>> AsignarUsuarioAEquipo(int idUsuario, int idEquipo)
        {
            var idUsuarioParam = new SqlParameter("@idUsuario", idUsuario);
            var idEquipoParam = new SqlParameter("@idEquipo", idEquipo);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC sp_Asignar_Usuario_A_Equipo @idUsuario, @idEquipo", idUsuarioParam, idEquipoParam)
                .ToListAsync();
        }
       

        public async Task<IEnumerable<MensajeUsuario>> ReactivarUsuario(int idUsuario)
        {
            var idUsuarioParam = new SqlParameter("@idUsuarios", idUsuario);

            return await _context.MensajeUsuario
                .FromSqlRaw("EXEC Reactivar_Usuario @idUsuarios", idUsuarioParam)
                .ToListAsync();
        }

        public async Task<string> RestablecerContrasena(int idUsuario)
        {
            // Generar la nueva contraseña
            string nuevaContrasena = _recursos.GenerarClave();
            string contrasenaEncriptada = _recursos.ConvertirSha256(nuevaContrasena);

            var idUsuarioParam = new SqlParameter("@idUsuarios", idUsuario);
            var contrasenaEncriptadaParam = new SqlParameter("@ContrasenaEncriptada", contrasenaEncriptada);

            // Llamar al procedimiento almacenado para actualizar la contraseña
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Restablecer_Contrasena @idUsuarios, @ContrasenaEncriptada",
                idUsuarioParam,
                contrasenaEncriptadaParam
            );

            return nuevaContrasena; // Retorna la contraseña generada para enviarla por correo
        }





    }
}
