using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosRequest
    {
        [Key]
        public int idComentarios { get; set; }

        [Required(ErrorMessage = "El campo Comentario es requerido.")]
        public string Comentario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        public bool Activo { get; set; }

        public int? Tareas_idTareas { get; set; }
        public int? idSubtareas { get; set; }
        public int? idProyectos { get; set; }
    }
}
