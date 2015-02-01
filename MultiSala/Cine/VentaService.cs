using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class VentaService : IVentaService
    {
        public IVentaRepository Repositorio { get; set; }

        public VentaService(IVentaRepository ventaRepositorio)
        {
            Repositorio = ventaRepositorio;
        }

        public VentasDTO Create(VentasDTO venta)
        {
            if (!Repositorio.ReadSesion(venta.SesionId).Abierto) //Comprobamos si la sesión de destino se encuentra abierta.
            {
                throw new Exception("Imposible vender entradas: La sesión se encuentra cerrada.");
            }
            else
            {
                if (!Repositorio.HayButacas(venta.NEntradas, venta.SesionId)) //Comprobamos si quedan butacas disponibles en la sesión de destino.
                {
                    throw new Exception("Imposible vender entradas: No quedan butacas disponibles.");
                }
                else
                {
                    venta.Precio = CalculaPrecio(venta);
                    return Repositorio.Create(venta);
                }
            }
        }


        public double CalculaPrecio(VentasDTO venta)

        {
            double total = 0;

            double NEntradasNoJoven = venta.NEntradas - venta.NEntradasJoven;

            if ((venta.NEntradas <= 0) || (venta.NEntradas < venta.NEntradasJoven) || (NEntradasNoJoven < 0))
            {
                throw new Exception("Venta incorrecta.");
            }
            else
            {
                if (NEntradasNoJoven >= 5)
                {
                    total = Constantes.PRECIO * (venta.NEntradasJoven * Constantes.DESCUENTOJOVEN + NEntradasNoJoven * Constantes.DESCUENTOGRUPO);
                }
                else
                {
                    total = Constantes.PRECIO * (venta.NEntradasJoven * Constantes.DESCUENTOJOVEN + NEntradasNoJoven);
                }
                return total;
            }
        }

        public double DevolverVenta(long idVenta)
        {
            return Repositorio.DevolverVenta(idVenta);
        }

        public VentasDTO Read(long id)
        {
            return Repositorio.Read(id);
        }

        public IList<VentasDTO> List()
        {
            return Repositorio.List();
        }

        public IList<VentasDTO> List(Sesion sesion)
        {
            return Repositorio.List(sesion);
        }

        public IList<VentasDTO> List(long idSesion)
        {
            return Repositorio.List(idSesion);
        }

        public VentasDTO Update(long idVenta, VentasDTO venta)
        {
            VentasDTO antiguaVenta = Repositorio.Read(venta.VentaId);
            if (antiguaVenta == null)
            {
                throw new Exception("Imposible actualizar entradas: No existe la venta a actualizar.");
            }
            if (!Repositorio.ReadSesion(venta.SesionId).Abierto) //Comprobamos si la sesión de destino se encuentra abierta.
            {
                throw new Exception("Imposible actualizar entradas: La sesión se encuentra cerrada.");
            }   
            if (!Repositorio.HayButacas(venta.NEntradas, venta.SesionId, antiguaVenta)) //Comprobamos si quedan butacas disponibles en la sesión de destino.
            {
                throw new Exception("Imposible actualizar entradas: No quedan butacas disponibles.");
            }
            venta.Precio = CalculaPrecio(venta);
            Repositorio.Update(idVenta, venta);
            venta.Cambio = venta.Precio - antiguaVenta.Precio;
            return venta;
        }

        public VentasDTO Delete(long id)
        {
            return Repositorio.Delete(id);
        }

        public double CalcularTotalVentas(DateTime fecha)
        {
            return Repositorio.CalcularTotalVentas(fecha);
        }

        public double CalcularTotalVentasSesion(long idSesion)
        {
            return Repositorio.CalcularTotalVentasSesion(idSesion);
        }

        public double CalcularTotalVentasSala(long idSala, DateTime fecha)
        {
            return Repositorio.CalcularTotalVentasSala(idSala, fecha);
        }

        public int CalcularEntradasVendidas(DateTime fecha)
        {
            return Repositorio.CalcularEntradasVendidas(fecha);
        }

        public int CalcularEntradasVendidasSesion(long idSesion)
        {
            return Repositorio.CalcularEntradasVendidasSesion(idSesion);
        }

        public int CalcularEntradasVendidasSala(long idSala, DateTime fecha)
        {
            return Repositorio.CalcularEntradasVendidasSala(idSala, fecha);
        }


        public int EntradasDisponibles(long sesionID)
        {
            return Repositorio.EntradasDisponibles(sesionID);
        }
    }
}