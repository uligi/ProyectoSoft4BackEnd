﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Portafolio
    {
        [Key]
        public int idPortafolio { get; set; }
        public string NombrePortafolio { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
    }
}
