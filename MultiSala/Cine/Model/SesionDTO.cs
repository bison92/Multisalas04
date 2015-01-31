using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class SesionDTO
    {
        public long SesionId { get; set; }

        public long SalaId { get; set; }
        public bool Abierto { get; set; }
        public DateTime fecha { get; set; }

        public SesionDTO() { }
    }
}