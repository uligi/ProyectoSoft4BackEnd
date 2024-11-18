using System;
using System.ComponentModel.DataAnnotations;

namespace Negocio.Modelos
{
    public class Permisos
    {
        [Key] public int idPermisos { get; set; }
        public string Nombre_Permisos { get; set; }
        public bool Activo { get; set; }
    }
}
