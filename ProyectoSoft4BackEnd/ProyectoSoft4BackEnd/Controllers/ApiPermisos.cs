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

        // Crear permiso
        [HttpPost("NuevoPermiso")]
        public async Task<IActionResult> NuevoPermiso([FromBody] Permisos permiso)
        {
            try
            {
                var resultado = await _service.CrearPermiso(permiso);

                if (resultado != null && resultado.Any())
                {
                    return Ok(resultado);
                }
                return BadRequest("No se pudo crear el permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Listar permisos
        [HttpGet("ListaPermisos")]
        public async Task<IActionResult> ListaPermisos()
        {
            try
            {
                var resultado = await _service.ObtenerPermisos();

                if (resultado != null && resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound("No se encontraron permisos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualizar permiso
        [HttpPut("ActualizarPermiso/{id}")]
        public async Task<IActionResult> ActualizarPermiso(int id, [FromBody] Permisos permiso)
        {
            try
            {
                var resultado = await _service.ActualizarPermiso(id, permiso.Nombre_Permisos, permiso.Activo);

                if (resultado != null && resultado.Any())
                {
                    return Ok(resultado);
                }
                return BadRequest("No se pudo actualizar el permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar permiso
        [HttpDelete("EliminarPermiso/{id}")]
        public async Task<IActionResult> EliminarPermiso(int id)
        {
            try
            {
                var resultado = await _service.EliminarPermiso(id);

                if (resultado != null && resultado.Any())
                {
                    return Ok(resultado);
                }
                return BadRequest("No se pudo eliminar el permiso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
