using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;

using System.Threading.Tasks;

namespace ProyectoSoft4BackEnd.Controllers
{ 

[Route("api/[controller]")]
[ApiController]
public class ApiMiembros : ControllerBase
{
    private readonly IMiembrosDeEquiposRepository _service;

    public ApiMiembros(IMiembrosDeEquiposRepository service)
    {
        _service = service;
    }

    [HttpGet("ListarMiembros/{idEquipos}")]
    public async Task<IActionResult> ListarMiembros(int idEquipos)
    {
        try
        {
            var resultado = await _service.ListarMiembrosEquipo(idEquipos);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("NuevoMiembro")]
    public async Task<IActionResult> NuevoMiembro([FromBody] MiembroEquipoRequest request)
    {
        try
        {
            var resultado = await _service.CrearMiembroEquipo(request.idEquipos, request.idUsuarios);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("EliminarMiembro/{idMiembro}")]
    public async Task<IActionResult> EliminarMiembro(int idMiembro)
    {
        try
        {
            var resultado = await _service.EliminarMiembroEquipo(idMiembro);
            return Ok(resultado);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 
}

