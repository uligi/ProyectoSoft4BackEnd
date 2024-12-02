using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Subtareas
    {
        [Key]
        public int idSubtareas { get; set; }
        public string NombreSubtareas { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
