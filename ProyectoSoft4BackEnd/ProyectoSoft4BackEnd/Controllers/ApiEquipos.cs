using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
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

        // Método para crear un nuevo equipo
        [HttpPost("NuevoEquipo")]
        public async Task<IActionResult> NuevoEquipo([FromBody] Equipos equipo)
        {
            try
            {
                var resultadoNuevoEquipo = await _service.CrearEquipo(equipo);

                if (resultadoNuevoEquipo != null && resultadoNuevoEquipo.Any())
                {
                    return Ok(resultadoNuevoEquipo);
                }
                return BadRequest("No se pudo crear el equipo.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de equipos
        [HttpGet("ListaEquipos")]
        public async Task<IActionResult> ListaEquipos()
        {
            try
            {
                var resultadoEquipos = await _service.ObtenerEquipos();

                if (resultadoEquipos != null && resultadoEquipos.Any())
                {
                    return Ok(resultadoEquipos);
                }
                return NotFound("No se encontraron equipos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un equipo
        [HttpPut("ActualizarEquipo/{id}")]
        public async Task<IActionResult> ActualizarEquipo(int id, [FromBody] Equipos equipo)
        {
            try
            {
                var resultadoActualizarEquipo = await _service.ActualizarEquipo(
                    id, equipo.NombreEquipos, equipo.Activo);

                if (resultadoActualizarEquipo != null && resultadoActualizarEquipo.Any())
                {
                    return Ok(resultadoActualizarEquipo);
                }
                return BadRequest("No se pudo actualizar el equipo.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
