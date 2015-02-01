using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public interface IVentaRepository
    {

        VentasDTO Create(VentasDTO venta);
        VentasDTO Read(long id);
        VentasDTO Update(long idVenta, VentasDTO venta);
        VentasDTO Delete(long id);

        IList<VentasDTO> List();
        IList<VentasDTO> List(Sesion sesion);
        IList<VentasDTO> List(long idSesion);

        Sesion ReadSesion(long idSesion);
        Sala ReadSala(long idSala);

        double DevolverVenta(long idVenta);
        bool HayButacas(int butacas, long sesionID, VentasDTO antiguaVenta = null);
        int EntradasDisponibles(long sesionID);

        double CalcularTotalVentas(DateTime fecha);
        double CalcularTotalVentasSesion(long idSesion);
        double CalcularTotalVentasSala(long idSala, DateTime fecha);
        int CalcularEntradasVendidas(DateTime fecha);
        int CalcularEntradasVendidasSesion(long idSesion);
        int CalcularEntradasVendidasSala(long idSala, DateTime fecha);


    }
}