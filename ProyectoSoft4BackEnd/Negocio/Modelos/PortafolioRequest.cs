using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class PortafolioRequest
    {
        [Required]
        public string NombrePortafolio { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;
    }
}
