using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRoles : ControllerBase
    {
        private readonly IRolesRepository _service;

        public ApiRoles(IRolesRepository service)
        {
            _service = service;
        }

        // Método para crear un nuevo rol
        [HttpPost("NuevoRol")]
        public async Task<IActionResult> NuevoRol([FromBody] Roles rol)
        {
            try
            {
                var resultadoNuevoRol = await _service.CrearRol(rol);

                if (resultadoNuevoRol != null && resultadoNuevoRol.Any())
                {
                    return Ok(resultadoNuevoRol);
                }
                return BadRequest("No se pudo crear el rol.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de roles
        [HttpGet("ListaRoles")]
        public async Task<IActionResult> ListaRoles()
        {
            try
            {
                var resultadoRoles = await _service.ObtenerRoles();

                if (resultadoRoles != null && resultadoRoles.Any())
                {
                    return Ok(resultadoRoles);
                }
                return NotFound("No se encontraron roles.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un rol
        [HttpPut("ActualizarRol/{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] Roles rol)
        {
            try
            {
                var resultadoActualizarRol = await _service.ActualizarRol(id, rol.Nombre_Roles, rol.Activo);

                if (resultadoActualizarRol != null && resultadoActualizarRol.Any())
                {
                    return Ok(resultadoActualizarRol);
                }
                return BadRequest("No se pudo actualizar el rol.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
