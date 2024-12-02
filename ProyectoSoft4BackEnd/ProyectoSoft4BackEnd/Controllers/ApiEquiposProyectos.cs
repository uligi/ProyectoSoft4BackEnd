using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEquiposProyectos : ControllerBase
    {
        private readonly IEquiposProyectosRepository _service;

        public ApiEquiposProyectos(IEquiposProyectosRepository service)
        {
            _service = service;
        }

        // Método para crear una nueva relación entre equipo y proyecto
        [HttpPost("NuevoEquipoProyecto")]
        public async Task<IActionResult> NuevoEquipoProyecto([FromBody] Equipos_Proyectos equipoProyecto)
        {
            try
            {
                var resultadoNuevoEquipoProyecto = await _service.CrearEquipoProyecto(equipoProyecto.Equipos_idEquipos, equipoProyecto.Proyectos_idProyectos);

                if (resultadoNuevoEquipoProyecto != null && resultadoNuevoEquipoProyecto.Any())
                {
                    return Ok(resultadoNuevoEquipoProyecto);
                }
                return BadRequest("No se pudo crear la relación entre el equipo y el proyecto.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de relaciones entre equipos y proyectos
        [HttpGet("ListaEquiposProyectos")]
        public async Task<IActionResult> ListaEquiposProyectos()
        {
            try
            {
                var resultadoEquiposProyectos = await _service.ObtenerEquiposProyectos();

                if (resultadoEquiposProyectos != null && resultadoEquiposProyectos.Any())
                {
                    return Ok(resultadoEquiposProyectos);
                }
                return NotFound("No se encontraron relaciones entre equipos y proyectos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar una relación entre un equipo y un proyecto
        [HttpPut("ActualizarEquipoProyecto/{id}")]
        public async Task<IActionResult> ActualizarEquipoProyecto(int id, [FromBody] Equipos_Proyectos equipoProyecto)
        {
            try
            {
                // Llamamos al servicio para actualizar la relación entre los equipos y proyectos
                var resultadoActualizarEquipoProyecto = await _service.ActualizarEquipoProyecto(
                    equipoProyecto.Equipos_idEquipos,
                    equipoProyecto.Proyectos_idProyectos
                );

                if (resultadoActualizarEquipoProyecto != null && resultadoActualizarEquipoProyecto.Any())
                {
                    return Ok(resultadoActualizarEquipoProyecto);
                }
                return BadRequest("No se pudo actualizar la relación entre el equipo y el proyecto.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
