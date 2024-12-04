using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   
      public class EquiposRequest
    {
        [Required]
        [StringLength(45)]
        public string NombreEquipos { get; set; } = string.Empty;
    }
}
