using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class MiembrosDeEquipos
    {
        [Key]
        public int idMiembros_de_equipos { get; set; }
        public int idEquipos { get; set; }
        public int idUsuarios { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreEquipos { get; set; }
    }

}
