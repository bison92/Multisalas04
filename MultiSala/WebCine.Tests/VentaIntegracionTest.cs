using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCine;
using Cine;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace CineTest
{
    [TestClass]
    public class VentaIntegracionTest
    {
        //private VentaRepository repositorio;
        //private VentaService servicio;
        //private VentaController sut;

        private VentasDTO venta1;
        private VentasDTO venta2;
        private VentasDTO venta3;
        private VentasDTO venta4;
        private VentasDTO venta5;
        private VentasDTO venta6;
        private VentasDTO venta7;
        private VentasDTO venta8;
        private VentasDTO venta9;
        private VentasDTO venta10;

        private IVentaController _controller;
        private ISesionController _sesionController;
        private IUnityContainer container;

        [TestInitialize]
        public void TestInicializa()
        {


            venta1 = new VentasDTO(1, 5, 0);
            venta2 = new VentasDTO(1, 3, 0);
            venta3 = new VentasDTO(2, 10, 0);

            venta4 = new VentasDTO(1, 93, 0);

            venta5 = new VentasDTO(1, 4, 0);
            venta6 = new VentasDTO(1, 5, 0);
            venta7 = new VentasDTO(1, 6, 0);

            venta8 = new VentasDTO(1, 4, 2);
            venta9 = new VentasDTO(1, 5, 1);
            venta10 = new VentasDTO(1, 6, 1);

            //repositorio = new VentaRepository();
            //servicio = new VentaService();
            //_controller = new VentaController();

            //servicio.Repositorio = repositorio;
            //_controller.Servicio = servicio;

            container = new UnityContainer();

            container.RegisterType(typeof(IVentaRepository), typeof(VentaRepository));
            container.RegisterType(typeof(IVentaService), typeof(VentaService));
            container.RegisterType(typeof(IVentaController), typeof(VentaController));

            container.RegisterType(typeof(ISesionRepository), typeof(SesionRepository));
            container.RegisterType(typeof(ISesionService), typeof(SesionService));
            container.RegisterType(typeof(ISesionController), typeof(SesionController));

            DatosDB v = new DatosDB();

            _controller = container.Resolve<IVentaController>();
            _sesionController = container.Resolve<ISesionController>();

            BorrarVentas();

            for (int i = 1; i < 9; i++)
            {
                _sesionController.Abrir(i);
            }
        }

        public void BorrarVentas()
        {
            using (var context = new DatosDB())
            {
                IList<Venta> Lista = context.Ventas.ToList<Venta>();
                foreach (Venta venta in Lista)
                {
                    context.Ventas.Remove(venta);
                }
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void TestDescuentoJoven()
        {
            VentasDTO resultado1 = _controller.Create(venta8);
            VentasDTO resultado2 = _controller.Create(venta9);
            VentasDTO resultado3 = _controller.Create(venta10);

            Assert.IsNotNull(resultado1.ID);
            Assert.IsNotNull(resultado2.ID);
            Assert.IsNotNull(resultado3.ID);
            Assert.AreEqual(3, _controller.List().Count);
            Assert.AreEqual(25.2, resultado1.Precio, 0.001);
            Assert.AreEqual(33.6, resultado2.Precio, 0.001);
            Assert.AreEqual(37.1, resultado3.Precio, 0.001);
        }

        [TestMethod]
        public void TestDescuentoGrupo()
        {
            VentasDTO resultado1 = _controller.Create(venta5);
            VentasDTO resultado2 = _controller.Create(venta6);
            VentasDTO resultado3 = _controller.Create(venta7);

            Assert.IsNotNull(resultado1.ID);
            Assert.IsNotNull(resultado2.ID);
            Assert.IsNotNull(resultado3.ID);
            Assert.AreEqual(3, _controller.List().Count);
            Assert.AreEqual(28.0, resultado1.Precio, 0.001);
            Assert.AreEqual(31.5, resultado2.Precio, 0.001);
            Assert.AreEqual(37.8, resultado3.Precio, 0.001);
        }

        [TestMethod]
        public void TestDevolucion()
        {
            VentasDTO resultado1 = _controller.Create(venta1);
            VentasDTO resultado2 = _controller.Create(venta2);
            VentasDTO resultado3 = _controller.Create(venta3);
            VentasDTO resultado4 = _controller.Create(venta8);

            double resultado5 = _controller.DevolverVenta(resultado2.ID);//21

            //56.7
            double resultado6 = _controller.CalcularTotalVentasSesion(resultado2.SesionID);
            double resultado7 = _controller.CalcularTotalVentasSesion(resultado3.SesionID);

            Assert.AreEqual(21.0, resultado5, 0.001);
            Assert.AreEqual(56.7, resultado6, 0.001);
            Assert.AreEqual(63, resultado7, 0.001);
        }











        [TestMethod]
        public void TestCreate()
        {

            //con descuento
            VentasDTO resultado1 = _controller.Create(venta1);//31.5

            //sin descuento
            VentasDTO resultado2 = _controller.Create(venta2);

            //con descuento
            VentasDTO resultado3 = _controller.Create(venta3);

            Assert.IsNotNull(resultado1.ID);
            Assert.IsNotNull(resultado2.ID);
            Assert.IsNotNull(resultado3.ID);
            Assert.AreEqual(3, _controller.List().Count);
            Assert.AreEqual(31.5, resultado1.Precio, 0.001);
            Assert.AreEqual(21.0, resultado2.Precio, 0.001);
            Assert.AreEqual(63.0, resultado3.Precio, 0.001);//sin descuento 70
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void TestCreate_NoHayButacas()
        {
            //la sesion 1 pertenece a la sala 1 la venta 1,2 y 4 pertenecen a la misma sesion sala con solo 100 butacas


            VentasDTO resultado1 = _controller.Create(venta1);//sesion1 5entradas
            VentasDTO resultado2 = _controller.Create(venta2);//sesion 1 3entradas

            VentasDTO resultado4 = _controller.Create(venta4);//sesion 1 93 entradas esta venta no pude realizarse no hay entradas suficientes


        }

        [TestMethod]
        public void TestUpdate_NoHayButacas()
        {
            //la sesion 1 pertenece a la sala 1 la venta 1,2 y 4 pertenecen a la misma sesion sala con solo 100 butacas

            _sesionController.Abrir(1);
            _sesionController.Abrir(2);

            VentasDTO resultado2 = _controller.Create(venta2);//sesion 1 3entradas
            VentasDTO resultado3 = _controller.Create(venta3);//sesion 2 10entradas --> venta de origen

            VentasDTO resultado4 = _controller.Create(venta4);//sesion 1 93 entradas -->DESTINO
            //(ORIGEN , DESTINO)
            VentasDTO result = _controller.Update(resultado3.ID, 1);
            //NO SE PUEDE REALIZAR EL CAMBIO PORQUE  nO HAY BUTACAS EN LA SESION DE DESTINO
            //EN LA SESION 1 hay 93+3=96 entradas vendidas y una capacidad de 100 no entran 10 entradas mas
            Assert.IsTrue(result == null);

        }

        [TestMethod]
        public void TestNumeroEntradasDisponibles()
        {
            _sesionController.Abrir(1);

            VentasDTO resultado2 = _controller.Create(venta2);//sesion 1 3entradas
            VentasDTO resultado4 = _controller.Create(venta4);//sesion 1 93 entradas -->DESTINO

            int res = _controller.EntradasDisponibles(resultado2.SesionID);
            Assert.AreEqual(4, res);
        }

        [TestMethod]
        public void TestCalcularTotalVentas()
        {


            VentasDTO resultado1 = _controller.Create(venta1);//31.5
            VentasDTO resultado2 = _controller.Create(venta2);//21
            VentasDTO resultado3 = _controller.Create(venta3);//63 SESION 2 fecha dia 13


            double totalVentas = _controller.CalcularTotalVentas(new DateTime(2015, 5, 12, 0,0, 0, 0));


            Assert.AreEqual(115.5, totalVentas, 0.001);

        }

        [TestMethod]
        public void TestDeleteVenta()
        {

            VentasDTO resultado1 = _controller.Create(venta1);//31.5
            VentasDTO resultado2 = _controller.Create(venta2);//21
            VentasDTO resultado3 = _controller.Create(venta3);//63

            double totalVentas = _controller.CalcularTotalVentas(new DateTime(2015, 5, 12, 17, 30, 0, 0));//de una fecha determinada

            Assert.AreEqual(115.5, totalVentas, 0.001);

            _controller.Delete(resultado2.ID);

            double totalVentasDev = _controller.CalcularTotalVentas(new DateTime(2015, 5, 12, 17, 30, 0, 0));


            Assert.AreEqual(63 + 31.5, totalVentasDev, 0.001);

        }

        [TestMethod]
        public void TestListVenta()
        {

            VentasDTO resultado1 = _controller.Create(venta1);//31.5
            VentasDTO resultado2 = _controller.Create(venta2);//21
            VentasDTO resultado3 = _controller.Create(venta3);//63

            double totalVentas = _controller.CalcularTotalVentas(new DateTime(2015, 5, 12, 17, 30, 0, 0));//de una fecha determinada 

            int cantidadVentas = _controller.List().Count;//devuele numero de ventas

            Assert.AreEqual(115.5, totalVentas, 0.001);
            Assert.IsTrue(cantidadVentas == 3);

            
            _controller.Delete(resultado2.ID);

            double totalVentasDev = _controller.CalcularTotalVentas(new DateTime(2015, 5, 12, 17, 30, 0, 0));//de una fecha determinada
            int cantidadVentasPosDelete = _controller.List().Count;//devuele numero de ventas despues de el borrado de una


            Assert.AreEqual(31.5+63, totalVentasDev, 0.001);

            Assert.IsTrue(cantidadVentasPosDelete == 2);
            
        }



        [TestMethod]
        public void TestCalcularTotalVentasSesion()
        {

            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2

            double totalVentasSesion = _controller.CalcularTotalVentasSesion(1);

            Assert.AreEqual(31.5 + 21, totalVentasSesion, 0.001);


        }


        [TestMethod]
        public void TestCalcularEntradasVendidas()
        {

            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1 sala 1 ,5 entradas
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1 sala 1, 3 entradas
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2 sala 1, 10 entradas

            double totalVentasSesion = _controller.CalcularEntradasVendidas(new DateTime(2015, 5, 12, 17, 30, 0, 0));

            Assert.AreEqual(18, totalVentasSesion, 0.001);


        }

        [TestMethod]
        public void TestCalcularTotalVentasSala()
        {

            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1 sala 1
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1 sala 1
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2 sala 1

            double totalVentasSesion = _controller.CalcularTotalVentasSala(1, new DateTime(2015, 5, 12, 17, 30, 0, 0));

            Assert.AreEqual(115.5, totalVentasSesion, 0.001);


        }


        [TestMethod]
        public void TestCalcularEntradasVendidasSesion()
        {

            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1 sala 1 ,5 entradas
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1 sala 1, 3 entradas
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2 sala 1, 10 entradas

            double totalVentasSesion = _controller.CalcularEntradasVendidasSesion(1);

            Assert.AreEqual(8, totalVentasSesion, 0.001);


        }


        [TestMethod]
        public void TestCalcularEntradasVendidasSala()
        {
            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1 sala 1 ,5 entradas
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1 sala 1, 3 entradas
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2 sala 1, 10 entradas

            double totalVentasSesion = _controller.CalcularEntradasVendidasSala(1, new DateTime(2015, 5, 12, 17, 30, 0, 0));

            Assert.AreEqual(18, totalVentasSesion, 0.001);
        }



        [TestMethod]
        public void CambioVentaSesionOrigenCerrrada()
        {
            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1 sala 1 ,5 entradas
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1 sala 1, 3 entradas
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2 sala 1, 10 entradas

           

            Assert.IsTrue(resultado1.ID > 0);
            Assert.AreEqual(1, resultado1.SesionID, 0.001);


            Sesion s = _sesionController.Cerrar(1);//origen

            long idVenta1 = resultado1.ID;
            VentasDTO result = _controller.Update(idVenta1, 1);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void CambioVenta()
        {
            Sesion s = _sesionController.Abrir(1);//origen
            VentasDTO resultado1 = _controller.Create(venta1);//31.5 sesion 1 sala 1 ,5 entradas
            VentasDTO resultado2 = _controller.Create(venta2);//21 sesion 1 sala 1, 3 entradas
            VentasDTO resultado3 = _controller.Create(venta3);//63 sesion 2 sala 1, 10 entradas

           // double totalVentasSesion = _controller.CalcularEntradasVendidasSala(1, new DateTime(2015, 5, 12, 17, 30, 0, 0));

            Assert.IsNotNull(resultado1.ID);
            Assert.AreEqual(1, resultado1.SesionID, 0.001);



            VentasDTO result = _controller.Update(resultado1.ID, 2);


            Assert.AreEqual(2, result.SesionID, 0.001);
        }


        [TestMethod]
        public void TestCalculaPrecio()
        {
            Sesion s = _sesionController.Abrir(2);//origen

            
            //VentasDTO resultado1 = _controller.Create(venta3);//63 sesion 2 sala 1, 10 entradas
            //VentasDTO resultado2 = _controller.Create(venta8);//(1,4,2) 2 normales, 2 con -20% = 25,2

            double res1 = _controller.CalculaPrecio(venta3);
            double res2 = _controller.CalculaPrecio(venta8);

            Assert.AreEqual(63, res1, 0.001);
            Assert.AreEqual(25.2, res2, 0.001);

        }

        [TestMethod]
        public void TestSacarSesionesDeFecha()
        {
            DateTime fechaConsulta = new DateTime(2015, 5, 13);
            IList<SesionDTO> resultados =_sesionController.List(fechaConsulta);
            int numeroResultados = resultados.Count();
            Assert.AreEqual(9, numeroResultados);


        }


    }
}