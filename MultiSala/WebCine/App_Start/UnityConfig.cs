using Cine;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using WebCine.Controllers;


namespace WebCine
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container = new UnityContainer();

            container.RegisterType(typeof(IVentaRepository), typeof(VentaRepository));
            container.RegisterType(typeof(IVentaService), typeof(VentaService));
            //container.RegisterType(typeof(IVentaController), typeof(VentaController));

            container.RegisterType(typeof(ISesionRepository), typeof(SesionRepository));
            container.RegisterType(typeof(ISesionService), typeof(SesionService));
            //container.RegisterType(typeof(ISesionController), typeof(SesionController));

            container.RegisterType(typeof(IValuesController), typeof(ValuesController));
            container.RegisterType(typeof(IValuesController), typeof(ValuesController));

           
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}