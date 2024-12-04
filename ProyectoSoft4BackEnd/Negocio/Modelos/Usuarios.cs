using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Usuarios
    {
        [Key]
        public int idUsuarios { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        public bool RestablecerContrasena { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int idRoles { get; set; }

        // Relación con Roles
        public Roles Rol { get; set; }
    }




}
