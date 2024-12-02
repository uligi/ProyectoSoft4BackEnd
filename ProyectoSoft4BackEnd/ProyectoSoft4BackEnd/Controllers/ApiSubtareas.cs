using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;  
using Negocio.Modelos;      

namespace ProyectoSoft4BackEnd.Controllers
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

        // Método para crear una nueva subtarea
        [HttpPost("NuevaSubtarea")]
        public async Task<IActionResult> NuevaSubtarea([FromBody] Subtareas subtarea)
        {
            try
            {
                var resultadoNuevaSubtarea = await _service.CrearSubtarea(subtarea);

                if (resultadoNuevaSubtarea != null && resultadoNuevaSubtarea.Any())
                {
                    return Ok(resultadoNuevaSubtarea);
                }
                return BadRequest("No se pudo crear la subtarea.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de subtareas
        [HttpGet("ListaSubtareas")]
        public async Task<IActionResult> ListaSubtareas()
        {
            try
            {
                var resultadoSubtareas = await _service.ObtenerSubtareas();

                if (resultadoSubtareas != null && resultadoSubtareas.Any())
                {
                    return Ok(resultadoSubtareas);
                }
                return NotFound("No se encontraron subtareas.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar una subtarea
        [HttpPut("ActualizarSubtarea/{id}")]
        public async Task<IActionResult> ActualizarSubtarea(int id, [FromBody] Subtareas subtarea)
        {
            try
            {
                var resultadoActualizarSubtarea = await _service.ActualizarSubtarea(id, subtarea.NombreSubtareas, subtarea.Descripcion, subtarea.Prioridad, subtarea.FechaInicio, subtarea.FechaFinal);

                if (resultadoActualizarSubtarea != null && resultadoActualizarSubtarea.Any())
                {
                    return Ok(resultadoActualizarSubtarea);
                }
                return BadRequest("No se pudo actualizar la subtarea.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
