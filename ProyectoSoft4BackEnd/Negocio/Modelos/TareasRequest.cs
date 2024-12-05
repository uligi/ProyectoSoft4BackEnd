using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class TareasRequest
    {
        [Key]
        public int idTareas { get; set; }
        public string NombreTareas { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int idProyectos { get; set; }
        public int? idUsuarios { get; set; }
        public string Estado { get; set; }
    }

}
