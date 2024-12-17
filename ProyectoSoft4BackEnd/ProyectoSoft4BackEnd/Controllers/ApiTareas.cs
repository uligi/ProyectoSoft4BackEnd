using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApiTareas : ControllerBase
{
    private readonly ITareasRepository _service;

    public ApiTareas(ITareasRepository service)
    {
        _service = service;
    }

    [HttpGet("ListarTareas")]
    public async Task<IActionResult> ListarTareas()
    {
        try
        {
            var tareas = await _service.ListarTareas();
            var tareasResponse = tareas.Select(t => new TareasResponse
            {
                idTareas = t.idTareas,
                NombreTareas = t.NombreTareas,
                Descripcion = t.Descripcion,
                Prioridad = t.Prioridad,
                FechaInicio = t.FechaInicio,
                FechaFinal = t.FechaFinal,
                Activo = t.Activo,
                NombreProyecto = t.NombreProyecto,
                NombreUsuario = t.NombreUsuario,
                Estado = t.Estado,
            });

            return Ok(tareasResponse);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("NuevaTarea")]
    public async Task<IActionResult> NuevaTarea([FromBody] TareasRequest tareaRequest)
    {
        try
        {
            var tarea = new TareasRequest
            {
                NombreTareas = tareaRequest.NombreTareas,
                Descripcion = tareaRequest.Descripcion,
                Prioridad = tareaRequest.Prioridad,
                FechaInicio = tareaRequest.FechaInicio,
                FechaFinal = tareaRequest.FechaFinal,
                idProyectos = tareaRequest.idProyectos,
                idUsuarios = tareaRequest.idUsuarios,
                Estado = tareaRequest.Estado,

            };

            var resultado = await _service.CrearTarea(tarea);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPut("ActualizarTarea/{id}")]
    public async Task<IActionResult> ActualizarTarea(int id, [FromBody] TareasRequest tarea)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tarea.Estado))
            {
                return BadRequest("El campo Estado es requerido.");
            }

            tarea.idTareas = id;
            var resultado = await _service.ActualizarTarea(tarea);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("EliminarTarea/{id}")]
    public async Task<IActionResult> EliminarTarea(int id)
    {
        try
        {
            var resultado = await _service.EliminarTarea(id);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListarTareasPorProyecto")]
    public async Task<IActionResult> ListarTareasPorProyecto(int idProyectos)
    {
        try
        {
            var tareas = await _service.ListarTareasPorProyecto(idProyectos);
            return Ok(tareas);
        }
        catch (System.Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("ObtenerTareaPorID")]
    public async Task<IActionResult> ObtenerTareaPorID(int idTarea)
    {
        try
        {
            var tarea = await _service.ObtenerTareaPorID(idTarea);
            if (tarea == null)
            {
                return NotFound($"No se encontró la tarea con ID: {idTarea}");
            }
            return Ok(tarea);
        }
        catch (System.Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }


    [HttpGet("ObtenerSubtareasPorTareaID")]
    public async Task<IActionResult> ObtenerSubtareasPorTareaID(int idTarea)
    {
        try
        {
            var subtareas = await _service.ObtenerSubtareasPorTareaID(idTarea);
            return Ok(subtareas);
        }
        catch (System.Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }



}
