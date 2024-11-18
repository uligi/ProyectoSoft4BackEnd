using Microsoft.AspNetCore.Mvc;
using Negocio.Controllers;
using Negocio.Modelos;
using System.Threading.Tasks;

namespace ProyectoINGSOFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUsuarios : ControllerBase
    {
        private readonly IUsuariosRepository _service;

        public ApiUsuarios(IUsuariosRepository service)
        {
            _service = service;
        }

        [HttpGet("ListaUsuarios")]
        public async Task<IActionResult> ListaUsuarios()
        {
            var resultadoUsuarios = await _service.ObtenerUsuarios();
            try
            {
                return Ok(resultadoUsuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NuevoUsuario")]
        public async Task<IActionResult> NuevoUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                var nuevoUsuario = await _service.CrearUsuario(usuario);
                return Ok(nuevoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
