using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Tareas
    {
        [Key]
        public int idTareas { get; set; }
        public string NombreTareas { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public int Subtareas_idSubtareas { get; set; }
        public int Proyectos_idProyectos { get; set; }
        public int Comentarios_idComentarios { get; set; }
    }
}
