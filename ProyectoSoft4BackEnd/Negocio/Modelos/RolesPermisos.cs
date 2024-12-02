using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class RolesPermisos
    {
        [Key]
        public int idRolesPermisos { get; set; }
        public int Permisos_idPermisos { get; set; }
        public int Roles_idRoles { get; set; }
    }
}
