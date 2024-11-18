using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{
    public class HistorialDeCambios
    {
        [Key] public int idHistorial_de_cambios { get; set; }
        public int Tareas_idTareas { get; set; }
        [ForeignKey("Tareas_idTareas")]
        public Tareas Tarea { get; set; }

        public int Proyectos_idProyectos { get; set; }
        [ForeignKey("Proyectos_idProyectos")]
        public Proyectos Proyecto { get; set; }

        public int Portafolio_idPortafolio { get; set; }
        [ForeignKey("Portafolio_idPortafolio")]
        public Portafolio Portafolio { get; set; }

        public string Descripcioncambio { get; set; }
        public DateTime FechaCambio { get; set; }
    }
}
