using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Subtareas
    {
        [Key]
        public int idSubtareas { get; set; }
        public string NombreSubtareas { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }

        public bool Activo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int idTareas { get; set; }
        public string NombreTarea { get; set; } // Para incluir el nombre de la tarea asociada

        public string Estado { get; set; }
    }
}
