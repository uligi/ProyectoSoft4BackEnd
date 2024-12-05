using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{

    public class TareasResponse
    {
        [Key]
        public int idTareas { get; set; }
        public string NombreTareas { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
        public string NombreProyecto { get; set; }
        public string NombreUsuario { get; set; }
        public string Estado { get; set; }
    }


}
