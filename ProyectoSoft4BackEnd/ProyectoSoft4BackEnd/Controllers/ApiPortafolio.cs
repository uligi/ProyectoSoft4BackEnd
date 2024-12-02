using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace NProyectoSoft4BackEndegocio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPortafolio : ControllerBase
    {
        private readonly IPortafolioRepository _service;

        public ApiPortafolio(IPortafolioRepository service)
        {
            _service = service;
        }

        // Método para crear un nuevo portafolio
        [HttpPost("NuevoPortafolio")]
        public async Task<IActionResult> NuevoPortafolio([FromBody] Portafolio portafolio)
        {
            try
            {
                var resultadoNuevoPortafolio = await _service.CrearPortafolio(portafolio);

                if (resultadoNuevoPortafolio != null && resultadoNuevoPortafolio.Any())
                {
                    return Ok(resultadoNuevoPortafolio);
                }
                return BadRequest("No se pudo crear el portafolio.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de portafolios
        [HttpGet("ListaPortafolios")]
        public async Task<IActionResult> ListaPortafolios()
        {
            try
            {
                var resultadoPortafolios = await _service.ObtenerPortafolios();

                if (resultadoPortafolios != null && resultadoPortafolios.Any())
                {
                    return Ok(resultadoPortafolios);
                }
                return NotFound("No se encontraron portafolios.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un portafolio
        [HttpPut("ActualizarPortafolio/{id}")]
        public async Task<IActionResult> ActualizarPortafolio(int id, [FromBody] Portafolio portafolio)
        {
            try
            {
                var resultadoActualizarPortafolio = await _service.ActualizarPortafolio(id, portafolio.NombrePortafolio, portafolio.Activo, portafolio.Descripcion);

                if (resultadoActualizarPortafolio != null && resultadoActualizarPortafolio.Any())
                {
                    return Ok(resultadoActualizarPortafolio);
                }
                return BadRequest("No se pudo actualizar el portafolio.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
