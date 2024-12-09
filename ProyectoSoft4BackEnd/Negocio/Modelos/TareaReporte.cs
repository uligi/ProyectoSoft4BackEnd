using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class TareaReporte
    {
        [Key]
        public int IdTareas { get; set; }
        public string NombreTareas { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Proyecto { get; set; } = string.Empty;// Nuevo campo para el nombre del proyecto asociado
        public string Equipo { get; set; } = string.Empty;// Nuevo campo para el nombre del equipo
        public string Portafolio { get; set; } = string.Empty; // Nuevo campo para el nombre del portafolio
    }
}
