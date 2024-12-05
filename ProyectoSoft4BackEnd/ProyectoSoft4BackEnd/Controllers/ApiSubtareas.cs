using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApiSubtareas : ControllerBase
{
    private readonly ISubtareasRepository _service;

    public ApiSubtareas(ISubtareasRepository service)
    {
        _service = service;
    }

    [HttpGet("ListarSubtareas")]
    public async Task<IActionResult> ListarSubtareas()
    {
        try
        {
            var subtareas = await _service.ListarSubtareas();
            return Ok(subtareas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("NuevaSubtarea")]
    public async Task<IActionResult> NuevaSubtarea([FromBody] SubtareasRequest subtareaRequest)
    {
        try
        {
            var resultado = await _service.CrearSubtarea(subtareaRequest);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ActualizarSubtarea/{id}")]
    public async Task<IActionResult> ActualizarSubtarea(int id, [FromBody] SubtareasRequest subtareaRequest)
    {
        try
        {
            var resultado = await _service.ActualizarSubtarea(id, subtareaRequest);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("EliminarSubtarea/{id}")]
    public async Task<IActionResult> EliminarSubtarea(int id)
    {
        try
        {
            var resultado = await _service.EliminarSubtarea(id);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
