using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class VentaRepository : IVentaRepository
    {

        public VentasDTO Create(VentasDTO venta)
        {
            Venta v=ventaDTOtoVenta(venta);
            using (var context = new DatosDB())
            {
                v = context.Ventas.Add(v);
                context.SaveChanges();
                venta= ventaToDTO(v);
            }


            return venta;
        }

        public VentasDTO Read(long idVenta)
        {
            Venta venta = null;
            VentasDTO DTO = null;
            using (var context = new DatosDB())
            {
                venta = context.Ventas.Find(idVenta);
                DTO = ventaToDTO(venta);
            }
            
            return DTO;
        }

       

        public VentasDTO Delete(long idVenta)
        {
            Venta venta = null;
            Venta devolucion;
            VentasDTO v = null;

            using (var context = new DatosDB())
            {
                devolucion = context.Ventas.Find(idVenta);
                venta = context.Ventas.Remove(devolucion);
                context.SaveChanges();
                 v = ventaToDTO(venta);
            }

            return v;
        }

        public Sesion ReadSesion(long idSesion)
        {
            Sesion sesion = null;
            using (var context = new DatosDB())
            {
                sesion = context.Sesiones.Find(idSesion);
            }
            return sesion;
        }

        public Sala ReadSala(long idSala)
        {
            Sala sala = null;
            using (var context = new DatosDB())
            {
                sala = context.Salas.Find(idSala);
            }
            return sala;
        }

        public double DevolverVenta(long idVenta)
        {
            double precio = 0;

            VentasDTO ventadto = null;
            ventadto = Read(idVenta);
             Venta venta = ventaDTOtoVenta(ventadto);

            if (ventadto != null) //Comprobación de que se ha realizado la lectura de la venta en la base de datos.
            {
                using (var context = new DatosDB())
                {
                    Devolucion devolucion = new Devolucion();

                    devolucion.ID = venta.ID;
                    devolucion.NEntradas = venta.NEntradas;
                    devolucion.NEntradasJoven = venta.NEntradasJoven;
                    devolucion.Precio = venta.Precio;
                    devolucion.SesionID = venta.SesionID;

                    context.Devoluciones.Add(devolucion);
                    context.SaveChanges();
                }
                precio = venta.Precio;
                Delete(venta.ID);
            }
            return precio;
        }






        private IList<VentasDTO> conversionLista(IList<Venta> listaVentas)
        {
            IList<VentasDTO> listaDTO = new List<VentasDTO>();
            foreach (Venta venta in listaVentas)
            {
                VentasDTO dto = new VentasDTO();
                dto.VentaId = venta.ID;
                dto.NEntradas = venta.NEntradas;
                dto.NEntradasJoven = venta.NEntradasJoven;
                dto.Precio = venta.Precio;

                dto.SesionId = venta.SesionID;

                listaDTO.Add(dto);
            }
            return listaDTO;
        }

        public VentasDTO ventaToDTO(Venta venta)
        {
            VentasDTO dto = new VentasDTO();
            if (venta != null)
            {
                dto.VentaId = venta.ID;
                dto.NEntradas = venta.NEntradas;
                dto.NEntradasJoven = venta.NEntradasJoven;
                dto.Precio = venta.Precio;

                dto.SesionId = venta.SesionID;
            }
            return dto;
        }



        private Venta ventaDTOtoVenta(VentasDTO ventaDto)
        {
            Venta v = new Venta();
            if (ventaDto != null)
            {
                v.ID = ventaDto.VentaId;
                v.NEntradas = ventaDto.NEntradas;
                v.NEntradasJoven = ventaDto.NEntradasJoven;
                v.Precio = ventaDto.Precio;

                v.SesionID = ventaDto.SesionId;
                v.Sesion = null;
            }
            return v;
        }

        public IList<VentasDTO> List()
        {
            IList<VentasDTO> listaDTO;
            using (var context = new DatosDB())
            {
                IList<Venta> ventaLista = context.Ventas.ToList<Venta>();
                listaDTO = conversionLista(ventaLista);
            }
            return listaDTO;
        }

        public IList<VentasDTO> List(Sesion sesion)
        {
            return List(sesion.SesionId);
        }

        public IList<VentasDTO> List(long idSesion)
        {
            IList<VentasDTO> listaDTO = new List<VentasDTO>();

            using (var context = new DatosDB())
            {
                IList<Venta> ventaLista = context.Ventas.ToList<Venta>();
                foreach (Venta venta in ventaLista)
                {
                    if (venta.SesionID == idSesion)
                    {
                        listaDTO.Add(ventaToDTO(venta));
                    }
                }
            }
            return listaDTO;//devuelve una lista  de las sesion en una sala
        }

        //cambio de entradas,implica cambio de sesion
        //venta ya existente id distinto de menos uno update
        // Venta Update(int idVenta,Sesion sesion);
        public VentasDTO Update(long idVenta, long idSesion)
        {
            VentasDTO cambio = null;
         
            Sesion sesionDB = ReadSesion(idSesion);//Sesión de destino

            if (sesionDB.Abierto) //Comprobamos si la sesión de destino se encuentra abierta.
            {
                if (ReadSesion(Read(idVenta).SesionId).Abierto) //Comprobamos si la sesión de origen se encuentra abierta.
                {
                    using (var context = new DatosDB())
                    {
                        Venta venta = context.Ventas.Find(idVenta);

                        if (HayButacas(venta.NEntradas, idSesion)) //Comprobamos si quedan butacas disponibles en la sesión de destino.
                        {
                            venta.SesionID = idSesion;
                            context.SaveChanges();
                            cambio = ventaToDTO(venta);
                        }
                    }
                }
            }
            return cambio;//devuelve objeto cambiado si existia una venta con ese id sino un null
        }



        /// <summary>
        /// Calcula el importe total de todas las ventas.
        /// </summary>
        /// <returns>Importe total en euros.</returns>
        public double CalcularTotalVentas(DateTime fecha)
        {
            double resultado = 0.0;

            DateTime fechaInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_INICIO, Constantes.MINUTO_INICIO, 0, 0);
            DateTime fechaFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_FIN, Constantes.MINUTO_FIN, 0, 0);

            using (var context = new DatosDB())
            {
                resultado = context.Database.SqlQuery<double>("select sum(v.precio) as totalVentas from Ventas as v,Sesions s,Salas sl where s.SalaID=sl.SalaID and v.SesionID=s.ID and s.fecha between  convert(datetime,@dateI) and convert(datetime,@dateF) ", new SqlParameter("dateI", fechaInicio), new SqlParameter("dateF", fechaFin)).FirstOrDefault();
            }
            return resultado;
        }

        /// <summary>
        /// Calcula el importe total de las ventas de una sesión dada.
        /// </summary>
        /// <param name="idSesion">Identificador de la sesión.</param>
        /// <returns>Importe total en euros.</returns>
        public double CalcularTotalVentasSesion(long idSesion)
        {
            double resultado = 0.0;
            Sesion s = ReadSesion(idSesion);
            DateTime fechaInicio = new DateTime(s.fecha.Year, s.fecha.Month, s.fecha.Day, Constantes.HORA_INICIO, Constantes.MINUTO_INICIO, 0, 0);
            DateTime fechaFin = new DateTime(s.fecha.Year, s.fecha.Month, s.fecha.Day, Constantes.HORA_FIN, Constantes.MINUTO_FIN, 0, 0);

            using (var context = new DatosDB())
            {
                resultado = context.Database.SqlQuery<double>("select sum(v.precio) as totalVentas from Ventas as v,Sesions s,Salas sl where s.SalaID=sl.SalaID and v.SesionID=s.ID and s.fecha between  convert(datetime,@dateI) and convert(datetime,@dateF) and s.ID=@idSesion ", new SqlParameter("dateI", fechaInicio), new SqlParameter("dateF", fechaFin), new SqlParameter("idSesion", idSesion)).FirstOrDefault();

            }
            return resultado;
        }

        /// <summary>
        /// Calcula el importe total de las ventas de todas las sesiones de una sala dada.
        /// </summary>
        /// <param name="idSala">Identificador de la sala.</param>
        /// <returns>Importe total en euros.</returns>
        public double CalcularTotalVentasSala(long idSala, DateTime fecha)
        {
            double resultado = 0.0d;

            DateTime fechaInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_INICIO, Constantes.MINUTO_INICIO, 0, 0);
            DateTime fechaFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_FIN, Constantes.MINUTO_FIN, 0, 0);

            using (var context = new DatosDB())
            {
                resultado = context.Database.SqlQuery<double>("select sum(v.precio) as totalVentas from Ventas as v,Sesions s,Salas sl where s.SalaID=sl.SalaID and v.SesionID=s.ID and s.fecha between  convert(datetime,@dateI) and convert(datetime,@dateF) and sl.SalaID=@idSala ", new SqlParameter("dateI", fechaInicio), new SqlParameter("dateF", fechaFin), new SqlParameter("idSala", idSala)).FirstOrDefault();
            }
            return resultado;
        }

        /// <summary>
        /// Calcula el número total de entradas vendidas.
        /// </summary>
        /// <returns>Número total de entradas vendidas.</returns>
        public int CalcularEntradasVendidas(DateTime fecha)
        {
            int resultado = 0;

            DateTime fechaInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_INICIO, Constantes.MINUTO_INICIO, 0, 0);
            DateTime fechaFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_FIN, Constantes.MINUTO_FIN, 0, 0);

            using (var context = new DatosDB())
            {
                resultado = context.Database.SqlQuery<int>("select sum(v.NEntradas) as totalEntradas from Ventas as v,Sesions s where  v.SesionID=s.ID and s.fecha between  convert(datetime,@dateI) and convert(datetime,@dateF) ", new SqlParameter("dateI", fechaInicio), new SqlParameter("dateF", fechaFin)).FirstOrDefault();

            }
            return resultado;
        }

        /// <summary>
        /// Calcula el número de entradas vendidas para la sesión dada.
        /// </summary>
        /// <param name="idSesion">Identificador de la sesión.</param>
        /// <returns>Número de entradas vendidas.</returns>
        public int CalcularEntradasVendidasSesion(long idSesion)
        {
            int resultado = 0;
            Sesion s = ReadSesion(idSesion);
            DateTime fechaInicio = new DateTime(s.fecha.Year, s.fecha.Month, s.fecha.Day, Constantes.HORA_INICIO, Constantes.MINUTO_INICIO, 0, 0);
            DateTime fechaFin = new DateTime(s.fecha.Year, s.fecha.Month, s.fecha.Day, Constantes.HORA_FIN, Constantes.MINUTO_FIN, 0, 0);

            using (var context = new DatosDB())
            {
                resultado = context.Database.SqlQuery<int>("select sum(v.NEntradas) as totalVentas from Ventas as v,Sesions s,Salas sl where s.SalaID=sl.SalaID and v.SesionID=s.ID and s.fecha between  convert(datetime,@dateI) and convert(datetime,@dateF) and s.ID=@idSesion group by s.ID", new SqlParameter("dateI", fechaInicio), new SqlParameter("dateF", fechaFin), new SqlParameter("idSesion", idSesion)).FirstOrDefault();
            }
            return resultado;
        }

        /// <summary>
        /// Calcula el número de entradas vendidas para la sala dada.
        /// </summary>
        /// <param name="idSala">Identificador de la sala.</param>
        /// <returns>Número de entradas vendidas.</returns>
        public int CalcularEntradasVendidasSala(long idSala, DateTime fecha)
        {
            int resultado = 0;

            DateTime fechaInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_INICIO, Constantes.MINUTO_INICIO, 0, 0);
            DateTime fechaFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, Constantes.HORA_FIN, Constantes.MINUTO_FIN, 0, 0);

            using (var context = new DatosDB())
            {
                resultado = context.Database.SqlQuery<int>("select sum(v.NEntradas) as totalVentas from Ventas as v,Sesions s,Salas sl where s.SalaID=sl.SalaID and v.SesionID=s.ID and s.fecha between  convert(datetime,@dateI) and convert(datetime,@dateF) and sl.SalaID=@idSala group by sl.SalaID", new SqlParameter("dateI", fechaInicio), new SqlParameter("dateF", fechaFin), new SqlParameter("idSala", idSala)).FirstOrDefault();
            }
            return resultado;
        }





        //public Sesion Cerrar(long id)
        //{
        //    Sesion s = ReadSesion(id);

        //    if (s.Abierto)
        //    {
        //        using (var context = new DatosDB())
        //        {
        //            s = context.Sesiones.Find(id);
        //            s.Abierto = false;

        //            context.SaveChanges();
        //        }
        //    }
        //    return s;
        //}

        //public Sesion Abrir(long id)
        //{
        //    Sesion s = ReadSesion(id);

        //    if (!s.Abierto)
        //    {

        //        using (var context = new DatosDB())
        //        {
        //            s = context.Sesiones.Find(id);
        //            s.Abierto = true;

        //            context.SaveChanges();
        //        }
        //    }
        //    return s;
        //}

        public bool HayButacas(int butacas, long sesionID)
        {
            int butacasVendidas = CalcularEntradasVendidasSesion(sesionID);
            //devuelve objeto sesion
            Sesion sesiondb = ReadSesion(sesionID);
            long id = sesiondb.SalaId;
            int aforo = ReadSala(id).NButacas;

            return (butacasVendidas + butacas <= aforo);
        }

        public int EntradasDisponibles(long sesionID)
        {
            int butacasVendidas = CalcularEntradasVendidasSesion(sesionID);
            Sesion sesiondb = ReadSesion(sesionID);
            long salaID = sesiondb.SalaId;
            int aforo = ReadSala(salaID).NButacas;

            return (aforo - butacasVendidas);
        }
    }
}