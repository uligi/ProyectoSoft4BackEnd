using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{
    public class RolesPermisos
    {
        [Key] public int idRolesPermisos { get; set; }

        public int Permisos_idPermisos { get; set; }
        [ForeignKey("Permisos_idPermisos")]
        public Permisos Permiso { get; set; }

        public int Roles_idRoles { get; set; }
        [ForeignKey("Roles_idRoles")]
        public Roles Rol { get; set; }
    }
}
