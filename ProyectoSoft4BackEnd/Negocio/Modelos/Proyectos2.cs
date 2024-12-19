using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Proyectos2
    {
        [Key]
        public int idProyectos { get; set; }

        [Required]
        [StringLength(500)]
        public string NombreProyecto { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public DateTime? FechaEstimada { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinal { get; set; }

        [StringLength(45)]
        public string Prioridad { get; set; } = string.Empty;

        public int idPortafolio { get; set; }
        public int Equipos_idEquipos { get; set; }
        public string Estado { get; set; }
        public string NombreEquipos { get; set; }
    }
}
