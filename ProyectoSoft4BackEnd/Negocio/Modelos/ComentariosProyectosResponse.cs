﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ComentariosProyectosResponse
    {
        [Key]
        public int idComentario { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int idProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }

}
