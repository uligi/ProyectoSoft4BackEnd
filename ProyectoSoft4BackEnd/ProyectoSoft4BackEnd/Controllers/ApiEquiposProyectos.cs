using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEquiposProyectos : ControllerBase
    {
        private readonly IEquiposProyectosRepository _service;

        public ApiEquiposProyectos(IEquiposProyectosRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaEquiposProyectos")]
        public async Task<IActionResult> ListaEquiposProyectos()
        {
            var resultadoEquiposProyectos = await _service.ObtenerEquiposProyectos();
            try
            {
                return Ok(resultadoEquiposProyectos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoEquipoProyecto")]
        public async Task<IActionResult> NuevoEquipoProyecto([FromBody] EquiposProyectos equipoProyecto)
        {
            try
            {
                var nuevoEquipoProyecto = await _service.CrearEquipoProyecto(equipoProyecto);
                return Ok(nuevoEquipoProyecto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
