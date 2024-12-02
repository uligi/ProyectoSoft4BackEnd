using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class MensajeUsuario
    {
        [Key] public int Codigo { get; set; }
        public string Mensaje { get; set; } = String.Empty;

    }
}
