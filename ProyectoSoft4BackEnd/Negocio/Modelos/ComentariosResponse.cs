using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosResponse
    {
        [Key]
        public int idComentarios { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int? Tareas_idTareas { get; set; }
        public int? idSubtareas { get; set; }
        public int? idProyectos { get; set; }
    }
}
