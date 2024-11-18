using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
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

        [HttpGet("ListaRoles")]
        public async Task<IActionResult> ListaRoles()
        {
            var resultadoRoles = await _service.ObtenerRoles();
            try
            {
                return Ok(resultadoRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoRol")]
        public async Task<IActionResult> NuevoRol([FromBody] Roles rol)
        {
            try
            {
                var nuevoRol = await _service.CrearRol(rol);
                return Ok(nuevoRol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
