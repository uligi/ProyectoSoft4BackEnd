using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApiComentariosProyectos : ControllerBase
{
    private readonly IComentariosProyectosRepository _service;

    public ApiComentariosProyectos(IComentariosProyectosRepository service)
    {
        _service = service;
    }

    [HttpGet("ListarComentarios")]
    public async Task<IActionResult> ListarComentarios()
    {
        try
        {
            var comentarios = await _service.ListarComentarios();
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AgregarComentario")]
    public async Task<IActionResult> AgregarComentario([FromBody] ComentariosProyectosRequest comentario)
    {
        try
        {
            var id = await _service.AgregarComentario(comentario);
            return Ok(new { idComentario = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ActualizarComentario/{id}")]
    public async Task<IActionResult> ActualizarComentario(int id, [FromBody] ComentariosProyectosRequest comentario)
    {
        try
        {
            comentario.idComentario = id;
            var result = await _service.ActualizarComentario(comentario);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("EliminarComentario/{id}")]
    public async Task<IActionResult> EliminarComentario(int id)
    {
        try
        {
            var result = await _service.EliminarComentario(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

