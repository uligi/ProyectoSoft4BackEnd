using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSubtareas : ControllerBase
    {
        private readonly ISubtareasRepository _service;

        public ApiSubtareas(ISubtareasRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaSubtareas")]
        public async Task<IActionResult> ListaSubtareas()
        {
            var resultadoSubtareas = await _service.ObtenerSubtareas();
            try
            {
                return Ok(resultadoSubtareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevaSubtarea")]
        public async Task<IActionResult> NuevaSubtarea([FromBody] Subtareas subtarea)
        {
            try
            {
                var nuevaSubtarea = await _service.CrearSubtarea(subtarea);
                return Ok(nuevaSubtarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
