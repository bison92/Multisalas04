using Cine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCine.Controllers
{
    public interface IVentaController
    {
       // VentasDTO Create(VentasDTO venta); //Crear venta = Vender
        VentasDTO Post(VentasDTO venta);
        //VentasDTO Read(long id); //Leer venta
        VentasDTO Get(int id);
        VentasDTO Put(long id, VentasDTO venta); //Cambiar la sesion de una venta
        VentasDTO Delete(long id); //Borrar venta

        IList<VentasDTO> GetList();
        IList<VentasDTO> List(Sesion sesion);
        IList<VentasDTO> List(long idSesion);

        double PostDevolverVenta(int id); //Devolver venta

        double GetCalculaPrecio(int idSesion, int nEntradas, int nEntradasJoven);


        double GetCalcularTotalVentas(int ano, int mes, int dia);
        double GetCalcularTotalVentasSesion(long idSesion);
        double GetCalcularTotalVentasSala(long idSala, int ano, int mes, int dia);
        int GetCalcularEntradasVendidas(int ano, int mes, int dia);
        int GetCalcularEntradasVendidasSesion(long idSesion);
        int GetCalcularEntradasVendidasSala(long idSala, int ano, int mes, int dia);

        int GetEntradasDisponibles(long sesionID);

    }
}