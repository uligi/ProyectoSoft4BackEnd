using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;  
using Negocio.Modelos;
using Negocio.Repositories;


namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUsuarios : ControllerBase
    {
        private readonly IUsuariosRepository _service;

        private readonly IRecursosRepository _recursos;

        public ApiUsuarios(IUsuariosRepository service, IRecursosRepository recursos)
        {
            _service = service;
            _recursos = recursos;
        }


        [HttpPost("NuevoUsuario")]
        public async Task<IActionResult> NuevoUsuario([FromBody] UsuarioRequest usuarioRequest)
        {
            try
            {
                Console.WriteLine($"Datos recibidos: Nombre={usuarioRequest.Nombre}, Email={usuarioRequest.Email}, idRoles={usuarioRequest.idRoles}");

                var usuario = new Usuarios
                {
                    Nombre = usuarioRequest.Nombre,
                    Email = usuarioRequest.Email,
                    idRoles = usuarioRequest.idRoles,
                    Activo = true,
                    RestablecerContrasena = true,
                    FechaRegistro = DateTime.Now
                };

                string claveGenerada = _recursos.GenerarClave();
                usuario.contrasena = _recursos.ConvertirSha256(claveGenerada);

                var resultadoNuevoUsuario = await _service.CrearUsuario(usuario);

                if (resultadoNuevoUsuario != null && resultadoNuevoUsuario.Any())
                {
                    string asunto = "Bienvenido al sistema";
                    string mensaje = $"Su usuario ha sido creado exitosamente. Su contraseña es: {claveGenerada}";

                    await _recursos.EnviarCorreo(usuario.Email, asunto, mensaje);

                    return Ok(resultadoNuevoUsuario);
                }

                return BadRequest("No se pudo crear el usuario.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }




        // Método para obtener la lista de usuarios
        [HttpGet("ListaUsuarios")]
        public async Task<IActionResult> ListaUsuarios()
        {
            try
            {
                var resultadoUsuarios = await _service.ObtenerUsuarios();

                if (resultadoUsuarios != null && resultadoUsuarios.Any())
                {
                    return Ok(resultadoUsuarios);
                }
                return NotFound("No se encontraron usuarios.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un usuario
        [HttpPut("ActualizarUsuario/{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioRequest usuarioRequest)
        {
            try
            {
                var resultadoActualizarUsuario = await _service.ActualizarUsuario(
                    id, usuarioRequest.Nombre, usuarioRequest.Email, usuarioRequest.idRoles);

                if (resultadoActualizarUsuario != null && resultadoActualizarUsuario.Any())
                {
                    return Ok(resultadoActualizarUsuario);
                }

                return BadRequest("No se pudo actualizar el usuario.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("EliminarUsuario/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var resultadoEliminarUsuario = await _service.EliminarUsuario(id);

                if (resultadoEliminarUsuario != null && resultadoEliminarUsuario.Any())
                {
                    return Ok(resultadoEliminarUsuario);
                }
                return BadRequest("No se pudo eliminar el usuario.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AsignarUsuarioAEquipo")]
        public async Task<IActionResult> AsignarUsuarioAEquipo([FromBody] AsignacionRequest request)
        {
            try
            {
                var resultado = await _service.AsignarUsuarioAEquipo(request.IdUsuario, request.IdEquipo);

                if (resultado != null && resultado.Any())
                {
                    return Ok(resultado);
                }
                return BadRequest("No se pudo asignar el usuario al equipo.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ReactivarUsuario/{id}")]
        public async Task<IActionResult> ReactivarUsuario(int id)
        {
            try
            {
                var resultadoReactivarUsuario = await _service.ReactivarUsuario(id);

                if (resultadoReactivarUsuario != null && resultadoReactivarUsuario.Any())
                {
                    return Ok(resultadoReactivarUsuario);
                }
                return BadRequest("No se pudo reactivar el usuario.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RestablecerContrasena/{id}")]
        public async Task<IActionResult> RestablecerContrasena(int id)
        {
            try
            {
                // Generar y restablecer la contraseña
                string nuevaContrasena = await _service.RestablecerContrasena(id);

                // Obtener datos del usuario
                var usuario = await _service.ObtenerUsuariosPorId(id);

                // Enviar la nueva contraseña al correo del usuario
                string asunto = "Restablecimiento de contraseña";
                string mensaje = $"Su nueva contraseña es: {nuevaContrasena}";

                await _recursos.EnviarCorreo(usuario.Email, asunto, mensaje);

                return Ok(new { Codigo = 1, Mensaje = "Contraseña restablecida y enviada al correo" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ListaUsuariosActivos")]
        public async Task<IActionResult> ListaUsuariosActivos()
        {
            try
            {
                var usuariosActivos = await _service.ObtenerUsuariosActivos();

                if (usuariosActivos == null || !usuariosActivos.Any())
                {
                    return NotFound("No se encontraron usuarios activos.");
                }

                return Ok(usuariosActivos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener usuarios activos: {ex.Message}");
            }
        }





    }
}
