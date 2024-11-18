using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
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

        [HttpGet("ListaPermisos")]
        public async Task<IActionResult> ListaPermisos()
        {
            var resultadoPermisos = await _service.ObtenerPermisos();
            try
            {
                return Ok(resultadoPermisos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoPermiso")]
        public async Task<IActionResult> NuevoPermiso([FromBody] Permisos permiso)
        {
            try
            {
                var nuevoPermiso = await _service.CrearPermiso(permiso);
                return Ok(nuevoPermiso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
