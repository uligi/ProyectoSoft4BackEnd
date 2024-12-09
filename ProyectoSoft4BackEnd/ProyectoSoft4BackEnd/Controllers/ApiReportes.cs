using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoSoft4BackEnd.Controllers
{
    [ApiController]
    [Route("api/Reportes")]
    public class ApiReportes : ControllerBase
    {
        private readonly ReportesRepository _repository;

        public ApiReportes(ReportesRepository repository)
        {
            _repository = repository;
        }

        // API para obtener reportes de Proyectos
        [HttpGet("Proyectos")]
        public async Task<IActionResult> GetProyectos(
            [FromQuery] int? idUsuario = null,
            [FromQuery] int? idEquipo = null,
            [FromQuery] int? idPortafolio = null)
        {
            try
            {
                var proyectos = await _repository.GetProyectos(idUsuario, idEquipo, idPortafolio);
                if (!proyectos.Any())
                {
                    return NotFound(new { mensaje = "No se encontraron proyectos." });
                }
                return Ok(proyectos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor.", detalle = ex.Message });
            }
        }

        // API para obtener reportes de Tareas
        [HttpGet("Tareas")]
        public async Task<IActionResult> GetTareas(
            [FromQuery] int? idUsuario = null,
            [FromQuery] int? idEquipo = null,
            [FromQuery] int? idPortafolio = null)
        {
            try
            {
                var tareas = await _repository.GetTareas(idUsuario, idEquipo, idPortafolio);
                if (!tareas.Any())
                {
                    return NotFound(new { mensaje = "No se encontraron tareas." });
                }
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor.", detalle = ex.Message });
            }
        }
    }
}
