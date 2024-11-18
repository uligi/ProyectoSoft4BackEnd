using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMiembrosDeEquipos : ControllerBase
    {
        private readonly IMiembrosDeEquiposRepository _service;

        public ApiMiembrosDeEquipos(IMiembrosDeEquiposRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaMiembrosDeEquipos")]
        public async Task<IActionResult> ListaMiembrosDeEquipos()
        {
            var resultadoMiembros = await _service.ObtenerMiembrosDeEquipos();
            try
            {
                return Ok(resultadoMiembros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoMiembroDeEquipo")]
        public async Task<IActionResult> NuevoMiembroDeEquipo([FromBody] MiembrosDeEquipos miembro)
        {
            try
            {
                var nuevoMiembro = await _service.CrearMiembroDeEquipo(miembro);
                return Ok(nuevoMiembro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
