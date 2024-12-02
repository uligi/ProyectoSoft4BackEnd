using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRolesPermisos : ControllerBase
    {
        private readonly IRolesPermisosRepository _service;

        public ApiRolesPermisos(IRolesPermisosRepository service)
        {
            _service = service;
        }

        // Método para crear una nueva relación entre rol y permiso
        [HttpPost("NuevoRolPermiso")]
        public async Task<IActionResult> NuevoRolPermiso([FromBody] RolesPermisos rolesPermisos)
        {
            try
            {
                var resultadoNuevoRolPermiso = await _service.CrearRolPermiso(rolesPermisos);

                if (resultadoNuevoRolPermiso != null && resultadoNuevoRolPermiso.Any())
                {
                    return Ok(resultadoNuevoRolPermiso);
                }
                return BadRequest("No se pudo crear la relación entre rol y permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de relaciones entre roles y permisos
        [HttpGet("ListaRolesPermisos")]
        public async Task<IActionResult> ListaRolesPermisos()
        {
            try
            {
                var resultadoRolesPermisos = await _service.ObtenerRolesPermisos();

                if (resultadoRolesPermisos != null && resultadoRolesPermisos.Any())
                {
                    return Ok(resultadoRolesPermisos);
                }
                return NotFound("No se encontraron relaciones entre roles y permisos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar una relación entre rol y permiso
        [HttpPut("ActualizarRolPermiso/{id}")]
        public async Task<IActionResult> ActualizarRolPermiso(int id, [FromBody] RolesPermisos rolesPermisos)
        {
            try
            {
                var resultadoActualizarRolPermiso = await _service.ActualizarRolPermiso(id, rolesPermisos.Permisos_idPermisos, rolesPermisos.Roles_idRoles);

                if (resultadoActualizarRolPermiso != null && resultadoActualizarRolPermiso.Any())
                {
                    return Ok(resultadoActualizarRolPermiso);
                }
                return BadRequest("No se pudo actualizar la relación entre rol y permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
