using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Miembros_de_equipos
    {
        [Key]
        public int idMiembros_de_equipos { get; set; }
        public int Equipos_idEquipos { get; set; }
        public int Usuarios_idUsuarios { get; set; }
        public int RolesPermisos_idRolesPermisos { get; set; }
    }
}
