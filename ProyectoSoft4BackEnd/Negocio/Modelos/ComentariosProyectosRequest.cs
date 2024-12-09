using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosProyectosRequest
    {
        
        public int idComentario { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int idProyecto { get; set; }
        public int idUsuario { get; set; }
    }

}
