using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosTareasResponse
    {
        [Key]
        public int idComentario { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int idTareas { get; set; }
        public string NombreTarea { get; set; } // Ajustar esto si es necesario
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }

}
