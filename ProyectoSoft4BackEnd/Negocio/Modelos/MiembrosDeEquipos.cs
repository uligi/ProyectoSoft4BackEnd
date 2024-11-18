using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{
    public class MiembrosDeEquipos
    {
        [Key] public int idMiembros_de_equipos { get; set; }

        public int Equipos_idEquipos { get; set; }
        [ForeignKey("Equipos_idEquipos")]
        public Equipos Equipo { get; set; }

        public int Usuarios_idUsuarios { get; set; }
        [ForeignKey("Usuarios_idUsuarios")]
        public Usuarios Usuario { get; set; }

        public int RolesPermisos_idRolesPermisos { get; set; }
        [ForeignKey("RolesPermisos_idRolesPermisos")]
        public RolesPermisos RolPermiso { get; set; }
    }
}
