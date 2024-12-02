using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiHistorialDeCambios : ControllerBase
    {
        private readonly IHistorialDeCambiosRepository _service;

        public ApiHistorialDeCambios(IHistorialDeCambiosRepository service)
        {
            _service = service;
        }

        // Método para crear un nuevo historial de cambios
        [HttpPost("NuevoHistorialDeCambio")]
        public async Task<IActionResult> NuevoHistorialDeCambio([FromBody] Historial_de_cambios historial)
        {
            try
            {
                var resultadoNuevoHistorial = await _service.CrearHistorialDeCambio(historial);

                if (resultadoNuevoHistorial != null && resultadoNuevoHistorial.Any())
                {
                    return Ok(resultadoNuevoHistorial);
                }
                return BadRequest("No se pudo crear el historial de cambios.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de historial de cambios
        [HttpGet("ListaHistorialDeCambios")]
        public async Task<IActionResult> ListaHistorialDeCambios()
        {
            try
            {
                var resultadoHistorial = await _service.ObtenerHistorialDeCambios();

                if (resultadoHistorial != null && resultadoHistorial.Any())
                {
                    return Ok(resultadoHistorial);
                }
                return NotFound("No se encontraron registros de historial de cambios.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un historial de cambio
        [HttpPut("ActualizarHistorialDeCambio/{id}")]
        public async Task<IActionResult> ActualizarHistorialDeCambio(int id, [FromBody] Historial_de_cambios historial)
        {
            try
            {
                var resultadoActualizarHistorial = await _service.ActualizarHistorialDeCambio(id, historial.Descripcioncambio, historial.FechaCambio);

                if (resultadoActualizarHistorial != null && resultadoActualizarHistorial.Any())
                {
                    return Ok(resultadoActualizarHistorial);
                }
                return BadRequest("No se pudo actualizar el historial de cambios.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
