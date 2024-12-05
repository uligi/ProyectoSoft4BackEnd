using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class ProyectosRequest
    {
        [Required]
        [StringLength(500)]
        public string NombreProyecto { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaEstimada { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }

        [Required]
        [StringLength(45)]
        public string Prioridad { get; set; } = string.Empty;

        [Required]
        public int idPortafolio { get; set; }

        [Required]
        public int Equipos_idEquipos { get; set; }
        public string Estado { get; set; }
    }
}
