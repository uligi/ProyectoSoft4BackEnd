using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Equipos_Proyectos
    {
        [Key]
        public int Equipos_idEquipos { get; set; }

        [Key]
        public int Proyectos_idProyectos { get; set; }
    }
}
