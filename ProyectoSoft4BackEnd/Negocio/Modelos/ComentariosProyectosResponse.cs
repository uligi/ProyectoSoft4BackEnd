using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosProyectosResponse
    {
        public int idComentario { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int idProyecto { get; set; }
        public string NombreProyecto { get; set; } = string.Empty;
        public int idUsuario { get; set; } // Validar esta propiedad
        public string NombreUsuario { get; set; } = string.Empty;
    }



}
