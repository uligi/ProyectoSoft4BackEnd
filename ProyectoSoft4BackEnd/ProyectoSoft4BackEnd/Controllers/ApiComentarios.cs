using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiComentarios : ControllerBase
    {
        private readonly IComentariosRepository _service;

        public ApiComentarios(IComentariosRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaComentarios")]
        public async Task<IActionResult> ListaComentarios()
        {
            var resultadoComentarios = await _service.ObtenerComentarios();
            try
            {
                return Ok(resultadoComentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoComentario")]
        public async Task<IActionResult> NuevoComentario([FromBody] Comentarios comentario)
        {
            try
            {
                var nuevoComentario = await _service.CrearComentario(comentario);
                return Ok(nuevoComentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
