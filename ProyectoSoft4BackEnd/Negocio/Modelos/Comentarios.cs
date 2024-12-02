using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Comentarios
    {
        [Key]
        public int idComentarios { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
