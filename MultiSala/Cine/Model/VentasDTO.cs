﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class VentasDTO
    {
        public long VentaId { get; set; }

        public long SesionId { get; set; }
        public int NEntradas { get; set; } //Número total de entradas vendidas.
        public int NEntradasJoven { get; set; } //Número de entradas vendidas de carnet joven.
        public double Precio { get; set; } //Precio del total de la venta.
        public double Cambio { get; set; }
        public VentasDTO() { }

        public VentasDTO(long sesionID, int numEntradas, int numEntradasJoven)
        {
            this.VentaId = -1;
            this.SesionId = sesionID;
            this.NEntradas = numEntradas;
            this.NEntradasJoven = numEntradasJoven;
            this.Precio = 0;
        }
    }
}