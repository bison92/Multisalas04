using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public interface IVentaService 
    {
        VentasDTO Create(VentasDTO venta);
        VentasDTO Read(long id);
        VentasDTO Update(long id, VentasDTO venta);
        VentasDTO Delete(long id);

        IList<VentasDTO> List();
        IList<VentasDTO> List(Sesion sesion);
        IList<VentasDTO> List(long idSesion);

        double DevolverVenta(long idVenta);

        double CalculaPrecio(VentasDTO venta);


        double CalcularTotalVentas(DateTime fecha);
        double CalcularTotalVentasSesion(long idSesion);
        double CalcularTotalVentasSala(long idSala, DateTime fecha);
        int CalcularEntradasVendidas(DateTime fecha);
        int CalcularEntradasVendidasSesion(long idSesion);
        int CalcularEntradasVendidasSala(long idSala, DateTime fecha);
        int EntradasDisponibles(long sesionID);

        
    }
}