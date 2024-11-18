using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTareas : ControllerBase
    {
        private readonly ITareasRepository _service;

        public ApiTareas(ITareasRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaTareas")]
        public async Task<IActionResult> ListaTareas()
        {
            var resultadoTareas = await _service.ObtenerTareas();
            try
            {
                return Ok(resultadoTareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevaTarea")]
        public async Task<IActionResult> NuevaTarea([FromBody] Tareas tarea)
        {
            try
            {
                var nuevaTarea = await _service.CrearTarea(tarea);
                return Ok(nuevaTarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
