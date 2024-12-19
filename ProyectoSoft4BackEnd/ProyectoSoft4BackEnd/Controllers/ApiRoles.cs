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
        public async Task<IActionResult> NuevoRol([FromBody] RolesRequest rolRequest)
        {
            try
            {
                // Mapear RolesRequest a Roles
                var rol = new Roles
                {
                    Nombre_Roles = rolRequest.Nombre,
                    Activo = true, // Por defecto los roles se crean como activos
                    idPermisos = rolRequest.idPermisos
                };

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
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] RolesRequest rolRequest)
        {
            try
            {
                // Mapear RolesRequest a Roles
                var resultadoActualizarRol = await _service.ActualizarRol(
                    id,
                    rolRequest.Nombre,
                    true, // Asume que siempre estará activo
                    rolRequest.idPermisos
                );

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


        [HttpDelete("EliminarRol/{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                var resultadoEliminarRol = await _service.EliminarRol(id);

                if (resultadoEliminarRol != null && resultadoEliminarRol.Any())
                {
                    return Ok(resultadoEliminarRol);
                }
                return BadRequest("No se pudo eliminar el rol.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ReactivarRol/{id}")]
        public async Task<IActionResult> ReactivarRol(int id)
        {
            try
            {
                var resultadoReactivarRol = await _service.ReactivarRol(id);

                if (resultadoReactivarRol != null && resultadoReactivarRol.Any())
                {
                    return Ok(resultadoReactivarRol);
                }
                return BadRequest("No se pudo reactivar el rol.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al reactivar el rol: {ex.Message}");
            }
        }


    }
}
