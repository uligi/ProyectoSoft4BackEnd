using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiProyectos : ControllerBase
    {
        private readonly IProyectosRepository _service;

        public ApiProyectos(IProyectosRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaProyectos")]
        public async Task<IActionResult> ListaProyectos()
        {
            var resultadoProyectos = await _service.ObtenerProyectos();
            try
            {
                return Ok(resultadoProyectos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoProyecto")]
        public async Task<IActionResult> NuevoProyecto([FromBody] Proyectos proyecto)
        {
            var resultadoNuevoProyecto = await _service.CrearProyecto(proyecto);
            try
            {
                return Ok(resultadoNuevoProyecto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
