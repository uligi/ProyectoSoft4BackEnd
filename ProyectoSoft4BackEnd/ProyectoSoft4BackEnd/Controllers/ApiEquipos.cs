using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEquipos : ControllerBase
    {
        private readonly IEquiposRepository _service;

        public ApiEquipos(IEquiposRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaEquipos")]
        public async Task<IActionResult> ListaEquipos()
        {
            var resultadoEquipos = await _service.ObtenerEquipos();
            try
            {
                return Ok(resultadoEquipos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoEquipo")]
        public async Task<IActionResult> NuevoEquipo([FromBody] Equipos equipo)
        {
            try
            {
                var nuevoEquipo = await _service.CrearEquipo(equipo);
                return Ok(nuevoEquipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
