using Microsoft.AspNetCore.Mvc;
using Negocio.Data;
using Negocio.Modelos;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ContextData _context;

    public AuthController(ContextData context)
    {
        _context = context;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Correo y contraseña son requeridos.");
        }

        string hashedPassword = ConvertirSha256(request.Password);

        var user = _context.Usuarios
            .Where(u => u.Email == request.Email && u.contrasena == hashedPassword && u.Activo)
            .Select(u => new
            {
                u.idUsuarios,
                u.Nombre,
                u.Email,
                u.RestablecerContrasena,
                u.idRoles,
                u.Rol.Nombre_Roles
            })
            .FirstOrDefault();

        if (user == null)
        {
            return Unauthorized("Credenciales inválidas.");
        }

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token, User = user });
    }

    private string ConvertirSha256(string texto)
    {
        using (SHA256 hash = SHA256.Create())
        {
            byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }

    private string GenerateJwtToken(dynamic user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ClaveSuperSeguraCon32CaracteresExactos")); // Cambia esta clave.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim("id", user.idUsuarios.ToString()),
            new Claim(ClaimTypes.Role, user.Nombre_Roles)
        };

        var token = new JwtSecurityToken(
            issuer: "tuapi.com",
            audience: "tuapi.com",
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("CambiarClave")]
    public IActionResult CambiarClave([FromBody] CambiarClaveRequest request)
    {
        if (string.IsNullOrEmpty(request.NuevaContrasena) || string.IsNullOrEmpty(request.ConfirmarContrasena))
        {
            return BadRequest("Todos los campos son requeridos.");
        }

        if (request.NuevaContrasena != request.ConfirmarContrasena)
        {
            return BadRequest("Las contraseñas no coinciden.");
        }

        if (!EsContrasenaValida(request.NuevaContrasena))
        {
            return BadRequest("La contraseña debe tener al menos 8 caracteres, una letra mayúscula, un carácter especial y un número.");
        }

        var usuario = _context.Usuarios.FirstOrDefault(u => u.idUsuarios == request.IdUsuario);

        if (usuario == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        usuario.contrasena = ConvertirSha256(request.NuevaContrasena);
        usuario.RestablecerContrasena = false; // Desactivar la bandera después del cambio
        _context.SaveChanges();

        return Ok("Contraseña actualizada correctamente.");
    }

    private bool EsContrasenaValida(string password)
    {
        var tieneMayuscula = password.Any(char.IsUpper);
        var tieneNumero = password.Any(char.IsDigit);
        var tieneEspecial = password.Any(ch => !char.IsLetterOrDigit(ch));
        return password.Length >= 8 && tieneMayuscula && tieneNumero && tieneEspecial;
    }


    [HttpGet("ObtenerPermisos")]
    public IActionResult ObtenerPermisos(int idUsuario)
    {
        var usuario = _context.Usuarios
            .Where(u => u.idUsuarios == idUsuario && u.Activo)
            .Select(u => new
            {
                u.idUsuarios,
                u.Nombre,
                Rol = u.Rol.Nombre_Roles,
                Permisos = u.Rol.Permiso.Nombre_Permisos
            })
            .FirstOrDefault();

        if (usuario == null)
        {
            return NotFound("Usuario no encontrado o inactivo.");
        }

        return Ok(usuario);
    }


}
