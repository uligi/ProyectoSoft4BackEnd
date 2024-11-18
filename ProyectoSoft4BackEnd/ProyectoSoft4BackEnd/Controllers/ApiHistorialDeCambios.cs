using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
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

        [HttpGet("ListaHistorialDeCambios")]
        public async Task<IActionResult> ListaHistorialDeCambios()
        {
            var resultadoHistorial = await _service.ObtenerHistorialDeCambios();
            try
            {
                return Ok(resultadoHistorial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoHistorialDeCambio")]
        public async Task<IActionResult> NuevoHistorialDeCambio([FromBody] HistorialDeCambios historial)
        {
            try
            {
                var nuevoHistorial = await _service.CrearHistorialDeCambio(historial);
                return Ok(nuevoHistorial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
