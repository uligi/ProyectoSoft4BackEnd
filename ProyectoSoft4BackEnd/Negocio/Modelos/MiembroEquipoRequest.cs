using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class MiembroEquipoRequest
    {
        [Required]
        public int idEquipos { get; set; }

        [Required]
        public int idUsuarios { get; set; }

        public bool forzar { get; set; }
    }
}
