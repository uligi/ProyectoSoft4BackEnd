using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMiembrosDeEquipos : ControllerBase
    {
        private readonly IMiembrosDeEquiposRepository _service;

        public ApiMiembrosDeEquipos(IMiembrosDeEquiposRepository service)
        {
            _service = service;
        }

        // Método para crear una nueva relación entre miembro y equipo
        [HttpPost("NuevoMiembroDeEquipo")]
        public async Task<IActionResult> NuevoMiembroDeEquipo([FromBody] Miembros_de_equipos miembro)
        {
            try
            {
                var resultadoNuevoMiembro = await _service.CrearMiembroDeEquipo(miembro);

                if (resultadoNuevoMiembro != null && resultadoNuevoMiembro.Any())
                {
                    return Ok(resultadoNuevoMiembro);
                }
                return BadRequest("No se pudo crear la relación entre el miembro y el equipo.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de miembros de equipos
        [HttpGet("ListaMiembrosDeEquipos")]
        public async Task<IActionResult> ListaMiembrosDeEquipos()
        {
            try
            {
                var resultadoMiembros = await _service.ObtenerMiembrosDeEquipos();

                if (resultadoMiembros != null && resultadoMiembros.Any())
                {
                    return Ok(resultadoMiembros);
                }
                return NotFound("No se encontraron miembros de equipos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar la relación de un miembro en un equipo
        [HttpPut("ActualizarMiembroDeEquipo/{id}")]
        public async Task<IActionResult> ActualizarMiembroDeEquipo(int id, [FromBody] Miembros_de_equipos miembro)
        {
            try
            {
                var resultadoActualizarMiembro = await _service.ActualizarMiembroDeEquipo(id, miembro.Equipos_idEquipos, miembro.Usuarios_idUsuarios, miembro.RolesPermisos_idRolesPermisos);

                if (resultadoActualizarMiembro != null && resultadoActualizarMiembro.Any())
                {
                    return Ok(resultadoActualizarMiembro);
                }
                return BadRequest("No se pudo actualizar la relación entre el miembro y el equipo.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
