using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;  
using Negocio.Modelos;      

namespace ProyectoSoft4BackEnd.Controllers
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

        // Método para crear una nueva tarea
        [HttpPost("NuevaTarea")]
        public async Task<IActionResult> NuevaTarea([FromBody] Tareas tarea)
        {
            try
            {
                var resultadoNuevaTarea = await _service.CrearTarea(tarea);

                if (resultadoNuevaTarea != null && resultadoNuevaTarea.Any())
                {
                    return Ok(resultadoNuevaTarea);
                }
                return BadRequest("No se pudo crear la tarea.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de tareas
        [HttpGet("ListaTareas")]
        public async Task<IActionResult> ListaTareas()
        {
            try
            {
                var resultadoTareas = await _service.ObtenerTareas();

                if (resultadoTareas != null && resultadoTareas.Any())
                {
                    return Ok(resultadoTareas);
                }
                return NotFound("No se encontraron tareas.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar una tarea
        [HttpPut("ActualizarTarea/{id}")]
        public async Task<IActionResult> ActualizarTarea(int id, [FromBody] Tareas tarea)
        {
            try
            {
                var resultadoActualizarTarea = await _service.ActualizarTarea(id, tarea.NombreTareas, tarea.Descripcion, tarea.Prioridad, tarea.FechaInicio, tarea.FechaFinal, tarea.Activo, tarea.Subtareas_idSubtareas, tarea.Proyectos_idProyectos, tarea.Comentarios_idComentarios);

                if (resultadoActualizarTarea != null && resultadoActualizarTarea.Any())
                {
                    return Ok(resultadoActualizarTarea);
                }
                return BadRequest("No se pudo actualizar la tarea.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
