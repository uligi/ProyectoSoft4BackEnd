using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class ProyectoReporte
    {
        [Key]
        public int IdProyectos { get; set; }
        public string NombreProyecto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Equipo { get; set; } = string.Empty;// Nuevo campo para el nombre del equipo
        public string Portafolio { get; set; } = string.Empty; // Nuevo campo para el nombre del portafolio
    }
}
