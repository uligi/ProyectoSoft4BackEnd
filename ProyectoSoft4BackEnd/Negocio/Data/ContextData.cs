using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Proyectos
    {
        [Key] public int idProyectos { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaEstimada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Prioridad { get; set; }
        public int Portafolio_idPortafolio { get; set; }
    }
}
