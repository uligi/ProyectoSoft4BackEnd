using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

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
        try
        {
            var resultado = await _service.ObtenerPortafolios();
            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }
            return NotFound("No se encontraron portafolios.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ListaPortafoliosActivos")]
    public async Task<IActionResult> ListaPortafoliosActivos()
    {
        try
        {
            var resultado = await _service.ObtenerPortafoliosActivos();
            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }
            return NotFound("No se encontraron portafolios.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("NuevoPortafolio")]
    public async Task<IActionResult> NuevoPortafolio([FromBody] PortafolioRequest portafolioRequest)
    {
        try
        {
            var portafolio = new Portafolio
            {
                NombrePortafolio = portafolioRequest.NombrePortafolio,
                Descripcion = portafolioRequest.Descripcion,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            var resultado = await _service.CrearPortafolio(portafolio);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ActualizarPortafolio/{id}")]
    public async Task<IActionResult> ActualizarPortafolio(int id, [FromBody] PortafolioRequest portafolioRequest)
    {
        try
        {
            var portafolio = new Portafolio
            {
                idPortafolio = id,
                NombrePortafolio = portafolioRequest.NombrePortafolio,
                Descripcion = portafolioRequest.Descripcion
            };

            var resultado = await _service.ActualizarPortafolio(portafolio);

            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }

            return NotFound("No se pudo actualizar el portafolio.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("EliminarPortafolio/{id}")]
    public async Task<IActionResult> EliminarPortafolio(int id)
    {
        try
        {
            var resultado = await _service.EliminarPortafolio(id);

            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }

            return NotFound("No se pudo eliminar el portafolio.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ReactivarPortafolio/{id}")]
    public async Task<IActionResult> ReactivarPortafolio(int id)
    {
        try
        {
            var resultado = await _service.ReactivarPortafolio(id);

            if (resultado != null && resultado.Any())
            {
                return Ok(resultado);
            }

            return NotFound("No se pudo reactivar el portafolio.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
