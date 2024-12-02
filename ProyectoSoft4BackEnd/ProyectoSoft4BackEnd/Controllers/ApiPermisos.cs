using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPermisos : ControllerBase
    {
        private readonly IPermisosRepository _service;

        public ApiPermisos(IPermisosRepository service)
        {
            _service = service;
        }

        // Método para crear un nuevo permiso
        [HttpPost("NuevoPermiso")]
        public async Task<IActionResult> NuevoPermiso([FromBody] Permisos permiso)
        {
            try
            {
                var resultadoNuevoPermiso = await _service.CrearPermiso(permiso);

                if (resultadoNuevoPermiso != null && resultadoNuevoPermiso.Any())
                {
                    return Ok(resultadoNuevoPermiso);
                }
                return BadRequest("No se pudo crear el permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de permisos
        [HttpGet("ListaPermisos")]
        public async Task<IActionResult> ListaPermisos()
        {
            try
            {
                var resultadoPermisos = await _service.ObtenerPermisos();

                if (resultadoPermisos != null && resultadoPermisos.Any())
                {
                    return Ok(resultadoPermisos);
                }
                return NotFound("No se encontraron permisos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un permiso
        [HttpPut("ActualizarPermiso/{id}")]
        public async Task<IActionResult> ActualizarPermiso(int id, [FromBody] Permisos permiso)
        {
            try
            {
                var resultadoActualizarPermiso = await _service.ActualizarPermiso(id, permiso.Nombre_Permisos, permiso.Activo);

                if (resultadoActualizarPermiso != null && resultadoActualizarPermiso.Any())
                {
                    return Ok(resultadoActualizarPermiso);
                }
                return BadRequest("No se pudo actualizar el permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
