using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class SalaDTO
    {
        public long SalaId { get; set; }

        public int Filas { get; set; }
        public int Columnas { get; set; }

        public int NButacas
        {
            get
            {
                return Filas * Columnas;
            }
            set
            {

            }
        }

        public SalaDTO() { }
    }
}