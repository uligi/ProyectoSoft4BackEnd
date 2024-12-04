using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class RolesRequest
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;


        [Required]
        public int idPermisos { get; set; }
    }
}
