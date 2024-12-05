using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApiComentarios : ControllerBase
{
    private readonly IComentariosRepository _service;

    public ApiComentarios(IComentariosRepository service)
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
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AgregarComentario")]
    public async Task<IActionResult> AgregarComentario([FromBody] ComentariosRequest comentario)
    {
        try
        {
            Console.WriteLine($"Comentario recibido: {System.Text.Json.JsonSerializer.Serialize(comentario)}");

            var id = await _service.AgregarComentario(comentario);
            return Ok(new { idComentarios = id });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("ActualizarComentario/{id}")]
    public async Task<IActionResult> ActualizarComentario(int id, [FromBody] ComentariosRequest comentario)
    {
        try
        {
            comentario.idComentarios = id;
            var result = await _service.ActualizarComentario(comentario);
            return Ok(result);
        }
        catch (System.Exception ex)
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
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
