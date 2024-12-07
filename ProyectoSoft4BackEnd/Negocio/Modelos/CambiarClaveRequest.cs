using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class CambiarClaveRequest
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NuevaContrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }

}
