using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
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

        [HttpGet("ListaRolesPermisos")]
        public async Task<IActionResult> ListaRolesPermisos()
        {
            var resultadoRolesPermisos = await _service.ObtenerRolesPermisos();
            try
            {
                return Ok(resultadoRolesPermisos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoRolPermiso")]
        public async Task<IActionResult> NuevoRolPermiso([FromBody] RolesPermisos rolPermiso)
        {
            try
            {
                var nuevoRolPermiso = await _service.CrearRolPermiso(rolPermiso);
                return Ok(nuevoRolPermiso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
