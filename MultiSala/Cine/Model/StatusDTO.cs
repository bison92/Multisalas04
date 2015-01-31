using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Model
{
    public class StatusDTO
    {
        public double Precio = Constantes.PRECIO;
        public int DescuentoHumbral = Constantes.DESCUENTOHUMBRAL;
        public double DescuentoGrupo = Constantes.DESCUENTOGRUPO;
        public double DescuentoGrupoJoven = Constantes.DESCUENTOJOVEN; 
        public StatusDTO() {
        }
    }
}