using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Roles
    {
        [Key]
        public int idRoles { get; set; }
        public string Nombre_Roles { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
