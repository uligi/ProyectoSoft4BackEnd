using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{
    public class Tareas
    {
        [Key] public int idTareas { get; set; }
        public string NombreTareas { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }

        public int? Subtareas_idSubtareas { get; set; }
        [ForeignKey("Subtareas_idSubtareas")]
        public Subtareas Subtarea { get; set; }

        public int Proyectos_idProyectos { get; set; }
        [ForeignKey("Proyectos_idProyectos")]
        public Proyectos Proyecto { get; set; }

        public int? Comentarios_idComentarios { get; set; }
        [ForeignKey("Comentarios_idComentarios")]
        public Comentarios Comentario { get; set; }
    }
}
