using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Equipos
    {
        [Key] public int IdEquipos { get; set; }
        public string? NombreEquipos { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
