using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;  
using Negocio.Modelos;      

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUsuarios : ControllerBase
    {
        private readonly IUsuariosRepository _service;

        public ApiUsuarios(IUsuariosRepository service)
        {
            _service = service;
        }

        // Método para crear un nuevo usuario
        [HttpPost("NuevoUsuario")]
        public async Task<IActionResult> NuevoUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                var resultadoNuevoUsuario = await _service.CrearUsuario(usuario);

                if (resultadoNuevoUsuario != null && resultadoNuevoUsuario.Any())
                {
                    return Ok(resultadoNuevoUsuario);
                }
                return BadRequest("No se pudo crear el usuario.");
            }
            catch (Exception ex)
            {
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
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] Usuarios usuario)
        {
            try
            {
                var resultadoActualizarUsuario = await _service.ActualizarUsuario(id, usuario.Nombre, usuario.Email, usuario.contrasena, usuario.Activo, usuario.FechaRegistro, usuario.Comentarios_idComentarios);

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
    }
}
