using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Historial_de_cambios
    {
        [Key]
        public int idHistorial_de_cambios { get; set; }
        public int Tareas_idTareas { get; set; }
        public int Proyectos_idProyectos { get; set; }
        public int Portafolio_idPortafolio { get; set; }
        public string Descripcioncambio { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; }
    }
}
