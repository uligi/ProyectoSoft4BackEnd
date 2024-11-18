using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Portafolio
    {
        [Key] public int idPortafolio { get; set; }
        public string NombrePortafolio { get; set; }
        public bool Activo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
