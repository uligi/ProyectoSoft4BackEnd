﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Equipos
    {
        [Key]
        public int idEquipos { get; set; }

        [Required]
        [StringLength(45)]
        public string NombreEquipos { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public DateTime Fecha_Registro { get; set; }
    }

}
