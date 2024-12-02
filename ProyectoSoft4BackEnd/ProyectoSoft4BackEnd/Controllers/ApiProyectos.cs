using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
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

        // Método para crear un nuevo proyecto
        [HttpPost("NuevoProyecto")]
        public async Task<IActionResult> NuevoProyecto([FromBody] Proyectos proyecto)
        {
            try
            {
                var resultadoNuevoProyecto = await _service.CrearProyecto(proyecto);

                if (resultadoNuevoProyecto != null && resultadoNuevoProyecto.Any())
                {
                    return Ok(resultadoNuevoProyecto);
                }
                return BadRequest("No se pudo crear el proyecto.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de proyectos
        [HttpGet("ListaProyectos")]
        public async Task<IActionResult> ListaProyectos()
        {
            try
            {
                var resultadoProyectos = await _service.ObtenerProyectos();

                if (resultadoProyectos != null && resultadoProyectos.Any())
                {
                    return Ok(resultadoProyectos);
                }
                return NotFound("No se encontraron proyectos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un proyecto
        [HttpPut("ActualizarProyecto/{id}")]
        public async Task<IActionResult> ActualizarProyecto(int id, [FromBody] Proyectos proyecto)
        {
            try
            {
                var resultadoActualizarProyecto = await _service.ActualizarProyecto(id, proyecto.NombreProyecto, proyecto.Descripcion, proyecto.Activo, proyecto.Prioridad, proyecto.FechaEstimada, proyecto.FechaInicio, proyecto.FechaFinal, proyecto.Portafolio_idPortafolio);

                if (resultadoActualizarProyecto != null && resultadoActualizarProyecto.Any())
                {
                    return Ok(resultadoActualizarProyecto);
                }
                return BadRequest("No se pudo actualizar el proyecto.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
