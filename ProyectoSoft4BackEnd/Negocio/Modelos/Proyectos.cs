using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class AVIONES
    {
        [Key] public int ID_AVION { get; set; }
        public string MATRICULA { get; set; } = string.Empty;
        public int ID_MARCA { get; set; }
        public string MODELO { get; set; } = string.Empty;
        public int NUM_ASIENTO { get; set; }

    }
}
