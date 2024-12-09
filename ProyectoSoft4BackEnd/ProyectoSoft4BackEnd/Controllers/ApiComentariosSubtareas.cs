using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApiComentariosSubtareas : ControllerBase
{
    private readonly IComentariosSubtareasRepository _service;

    public ApiComentariosSubtareas(IComentariosSubtareasRepository service)
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
    public async Task<IActionResult> AgregarComentario([FromBody] ComentariosSubtareasRequest comentario)
    {
        try
        {
            var id = await _service.AgregarComentario(comentario);
            return Ok(new { idComentario = id });
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ActualizarComentario/{id}")]
    public async Task<IActionResult> ActualizarComentario(int id, [FromBody] ComentariosSubtareasRequest comentario)
    {
        try
        {
            comentario.idComentario = id;
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

    //****************************************Por Sub'Tarea****************************************


    [HttpGet("ListarComentariosPorSubTarea")]
    public async Task<IActionResult> ListarComentariosPorSubTarea([FromQuery] int idSubtarea)
    {
        try
        {
            var comentarios = await _service.ListarComentariosPorSubTarea(idSubtarea); // Cambiar idTarea a idSubtarea
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

}
