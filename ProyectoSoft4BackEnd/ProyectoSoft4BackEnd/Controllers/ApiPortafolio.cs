using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
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

        [HttpGet("ListaPortafolios")]
        public async Task<IActionResult> ListaPortafolios()
        {
            var resultadoPortafolios = await _service.ObtenerPortafolios();
            try
            {
                return Ok(resultadoPortafolios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoPortafolio")]
        public async Task<IActionResult> NuevoPortafolio([FromBody] Portafolio portafolio)
        {
            try
            {
                var nuevoPortafolio = await _service.CrearPortafolio(portafolio);
                return Ok(nuevoPortafolio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
