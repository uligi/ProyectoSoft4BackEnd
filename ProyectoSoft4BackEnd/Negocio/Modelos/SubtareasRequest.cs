using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class SubtareasRequest
    {
        [Required]
        public int idSubtareas { get; set; }
        public string NombreSubtareas { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        [Required]
        public int idTareas { get; set; }
        public string Estado { get; set; }
    }
}
