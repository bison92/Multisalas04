using Cine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebCine.Controllers
{
    public class VentaController : ApiController, IVentaController
    {

        public IVentaService Servicio { get; set; }

        public VentaController(IVentaService ventaService)
        {
            Servicio = ventaService;
        }

        // POST api/venta
        //public VentasDTO Create(VentasDTO venta)
        [Route("api/venta")]
        public VentasDTO Post(VentasDTO venta)
        {
            VentasDTO ventaController = Servicio.Create(venta);
            return ventaController;
        }

        //Post /api/venta/devolverVenta/1
        [Route("api/venta/devolverVenta/{id:int}")]
        public double PostDevolverVenta(int id)
        {
            return Servicio.DevolverVenta(id);
        }

        // GET api/values/5
        //public VentasDTO Read(long id)
        [Route("api/venta/{id:int}")]
        public VentasDTO Get(int id)
        {
            return Servicio.Read(id);
        }

        [Route("api/venta/listado")]
        public IList<VentasDTO> GetList()
        {
            return Servicio.List();
        }

        public IList<VentasDTO> List(Sesion sesion)
        {
            return Servicio.List(sesion);
        }

        public IList<VentasDTO> List(long idSesion)
        {
            return Servicio.List(idSesion);
        }

        // PUT api/venta/{id:int}
        [Route("api/venta/{id:int}")]
        public VentasDTO Put(long id, VentasDTO venta)
        {
            return Servicio.Update(id, venta.SesionId);
        }

        public VentasDTO Delete(long id)
        {
            return Servicio.Delete(id);
        }
        [Route("api/venta/calculaPrecio/{idSesion:int}/{nEntradas:int}/{nEntradasJoven}")]
        public double GetCalculaPrecio(int idSesion, int nEntradas, int nEntradasJoven)
        {
            VentasDTO venta = new VentasDTO(idSesion, nEntradas, nEntradasJoven);
            return Servicio.CalculaPrecio(venta);
        }

         [Route("api/totalventa/{ano:int}/{mes:int}/{dia:int}")]
        public double GetCalcularTotalVentas(int ano,int mes,int dia)
        {

            return Servicio.CalcularTotalVentas(new DateTime(ano, mes, dia));
        }

        [Route("api/totalventasesion/{idSesion:long}")]
        public double GetCalcularTotalVentasSesion(long idSesion)
        {
            return Servicio.CalcularTotalVentasSesion(idSesion);
        }
         [Route("api/totalventasala/{idSala:long}/{ano:int}/{mes:int}/{dia:int}")]
        public double GetCalcularTotalVentasSala(long idSala, int ano, int mes, int dia)
        {
            return Servicio.CalcularTotalVentasSala(idSala, new DateTime(ano, mes, dia));
        }
         [Route("api/totalentrada/{ano:int}/{mes:int}/{dia:int}")]
         public int GetCalcularEntradasVendidas(int ano, int mes, int dia)
        {
            return Servicio.CalcularEntradasVendidas(new DateTime(ano, mes, dia));
        }
         [Route("api/totalentradasesion/{idSesion:long}")]
        public int GetCalcularEntradasVendidasSesion(long idSesion)
        {
            return Servicio.CalcularEntradasVendidasSesion(idSesion);
        }
         [Route("api/totalentradasala/{idSala:long}/{ano:int}/{mes:int}/{dia:int}")]
         public int GetCalcularEntradasVendidasSala(long idSala, int ano, int mes, int dia)
        {
            return Servicio.CalcularEntradasVendidasSala(idSala, new DateTime(ano, mes, dia));
        }

        [Route("api/venta/entradasDisponibles/{sesionID:int}")]
        public int GetEntradasDisponibles(long sesionID)
        {

            return Servicio.EntradasDisponibles(sesionID);
        }
    }
}