using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class Sala
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

        public virtual ICollection<Sesion> Sesiones { get; set; }

        public Sala() { }
    }
}