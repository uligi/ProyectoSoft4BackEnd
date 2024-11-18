using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negocio.Modelos
{
    public class Usuarios
    {
        [Key] public int idUsuarios { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public int? Comentarios_idComentarios { get; set; }
        [ForeignKey("Comentarios_idComentarios")]
        public Comentarios Comentario { get; set; }
    }
}
