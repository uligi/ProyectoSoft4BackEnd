using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;

namespace ProyectoSoft4BackEnd.Controllers
{
    [ApiController]
    [Route("api/Reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly ReportesRepository _repository;

        public ReportesController(ReportesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Proyectos")]
        public async Task<IActionResult> GetProyectos([FromQuery] DateTime fechaInicio, [FromQuery] string estado)
        {
            var proyectos = await _repository.GetProyectos(fechaInicio, estado);
            return Ok(proyectos);
        }

        [HttpGet("Tareas")]
        public async Task<IActionResult> GetTareas([FromQuery] int idUsuario, [FromQuery] string prioridad)
        {
            var tareas = await _repository.GetTareas(idUsuario, prioridad);
            return Ok(tareas);
        }
    }

}
