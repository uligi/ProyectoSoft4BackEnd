using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{
    public class EquiposProyectos
    {
        [Key]
        [Column(Order = 0)]
        public int Equipos_idEquipos { get; set; }
        [ForeignKey("Equipos_idEquipos")]
        public Equipos Equipo { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Proyectos_idProyectos { get; set; }
        [ForeignKey("Proyectos_idProyectos")]
        public Proyectos Proyecto { get; set; }
    }
}
