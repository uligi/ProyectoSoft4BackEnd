using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Controllers.Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApiEquipos : ControllerBase
{
    private readonly IEquiposRepository _service;

    public ApiEquipos(IEquiposRepository service)
    {
        _service = service;
    }

    [HttpGet("ListaEquipos")]
    public async Task<IActionResult> ListaEquipos()
    {
        try
        {
            var resultado = await _service.ObtenerEquipos();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("ListaEquiposActivos")]
    public async Task<IActionResult> ListaEquiposActivos()
    {
        try
        {
            var resultado = await _service.ObtenerEquiposActivos();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("NuevoEquipo")]
    public async Task<IActionResult> NuevoEquipo([FromBody] EquiposRequest request)
    {
        try
        {
            var equipo = new Equipos
            {
                NombreEquipos = request.NombreEquipos,
                Activo = true,
                Fecha_Registro = DateTime.Now
            };

            var resultado = await _service.CrearEquipo(equipo);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ActualizarEquipo/{id}")]
    public async Task<IActionResult> ActualizarEquipo(int id, [FromBody] EquiposRequest request)
    {
        try
        {
            var resultado = await _service.ActualizarEquipo(id, request.NombreEquipos);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("EliminarEquipo/{id}")]
    public async Task<IActionResult> EliminarEquipo(int id)
    {
        try
        {
            var resultado = await _service.EliminarEquipo(id);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ReactivarEquipos/{id}")]
    public async Task<IActionResult> ReactivarEquipos(int id)
    {
        try
        {
            var resultado = await _service.ReactivarEquipos(id);

            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }

            return NotFound("No se pudo reactivar el Equipo.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
