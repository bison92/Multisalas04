using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class Venta
    {
        public long ID { get; set; }

        public long SesionID { get; set; }
        public int NEntradas { get; set; } //Número total de entradas vendidas.
        public int NEntradasJoven { get; set; } //Número de entradas vendidas de carnet joven.
        public double Precio { get; set; } //Precio del total de la venta.

        public virtual Sesion Sesion { get; set; }

        public Venta() { }

        public Venta(long sesionID, int numEntradas, int numEntradasJoven)
        {
            this.ID = -1;
            this.SesionID = sesionID;
            this.NEntradas = numEntradas;
            this.NEntradasJoven = numEntradasJoven;
            this.Precio = 0;
        }
    }
}