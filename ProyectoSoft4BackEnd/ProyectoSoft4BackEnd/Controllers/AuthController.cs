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
}
