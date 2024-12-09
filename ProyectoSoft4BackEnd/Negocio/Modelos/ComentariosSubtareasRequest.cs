using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosSubtareasRequest
    {
        [Key]
        public int idComentario { get; set; }
        [Required]
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int idSubtarea { get; set; }
        public int idUsuario { get; set; }
    }
}
