using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

namespace ProyectoSoft4BackEnd.Controllers
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

        // Método para crear un nuevo comentario
        [HttpPost("NuevoComentario")]
        public async Task<IActionResult> NuevoComentario([FromBody] Comentarios comentario)
        {
            try
            {
                var resultadoNuevoComentario = await _service.CrearComentario(comentario);

                if (resultadoNuevoComentario != null && resultadoNuevoComentario.Any())
                {
                    return Ok(resultadoNuevoComentario);
                }
                return BadRequest("No se pudo crear el comentario.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para obtener la lista de comentarios
        [HttpGet("ListaComentarios")]
        public async Task<IActionResult> ListaComentarios([FromQuery] string textoComentario)
        {
            try
            {
                var resultadoComentarios = await _service.ObtenerComentarios(textoComentario);

                if (resultadoComentarios != null && resultadoComentarios.Any())
                {
                    return Ok(resultadoComentarios);
                }
                return NotFound("No se encontraron comentarios.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un comentario
        [HttpPut("ActualizarComentario/{id}")]
        public async Task<IActionResult> ActualizarComentario(int id, [FromBody] Comentarios comentario)
        {
            try
            {
                var resultadoActualizarComentario = await _service.ActualizarComentario(id, comentario.Comentario, comentario.Activo);

                if (resultadoActualizarComentario != null && resultadoActualizarComentario.Any())
                {
                    return Ok(resultadoActualizarComentario);
                }
                return BadRequest("No se pudo actualizar el comentario.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
