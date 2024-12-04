using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        try
        {
            var resultado = await _service.ObtenerProyectos();
            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }
            return NotFound("No se encontraron proyectos.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("NuevoProyecto")]
    public async Task<IActionResult> NuevoProyecto([FromBody] ProyectosRequest proyectoRequest)
    {
        try
        {
            if (proyectoRequest == null || string.IsNullOrWhiteSpace(proyectoRequest.NombreProyecto))
            {
                return BadRequest("El proyecto es inválido o tiene campos faltantes.");
            }

            var proyecto = new Proyectos
            {
                NombreProyecto = proyectoRequest.NombreProyecto,
                Descripcion = proyectoRequest.Descripcion,
                FechaEstimada = proyectoRequest.FechaEstimada,
                FechaInicio = proyectoRequest.FechaInicio,
                FechaFinal = proyectoRequest.FechaFinal,
                Prioridad = proyectoRequest.Prioridad,
                idPortafolio = proyectoRequest.idPortafolio,
                Equipos_idEquipos = proyectoRequest.Equipos_idEquipos,
                Activo = true
            };

            var resultado = await _service.CrearProyecto(proyecto);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }


    [HttpPut("ActualizarProyecto/{id}")]
    public async Task<IActionResult> ActualizarProyecto(int id, [FromBody] ProyectosRequest proyectoRequest)
    {
        try
        {
            var proyecto = new Proyectos
            {
                idProyectos = id,
                NombreProyecto = proyectoRequest.NombreProyecto,
                Descripcion = proyectoRequest.Descripcion,
                FechaEstimada = proyectoRequest.FechaEstimada,
                FechaInicio = proyectoRequest.FechaInicio,
                FechaFinal = proyectoRequest.FechaFinal,
                Prioridad = proyectoRequest.Prioridad,
                idPortafolio = proyectoRequest.idPortafolio,
                Equipos_idEquipos = proyectoRequest.Equipos_idEquipos
            };

            var resultado = await _service.ActualizarProyecto(proyecto);

            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }

            return NotFound("No se pudo actualizar el proyecto.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("EliminarProyecto/{id}")]
    public async Task<IActionResult> EliminarProyecto(int id)
    {
        try
        {
            var resultado = await _service.EliminarProyecto(id);

            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }

            return NotFound("No se pudo eliminar el proyecto.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
