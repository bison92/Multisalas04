using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class Sesion
    {
        public long SesionId { get; set; }

        public long SalaId { get; set; }
        public bool Abierto { get; set; }
        public DateTime fecha { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
        public virtual ICollection<Devolucion> Devoluciones { get; set; }

        public virtual Sala Sala { get; set; }

        public Sesion() { }
    }
}