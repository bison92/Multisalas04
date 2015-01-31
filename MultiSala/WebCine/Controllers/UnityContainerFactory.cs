using Microsoft.Practices.Unity;
//using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cine;

namespace WebCine.Controllers
{
    public class UnityContainerFactory
    {

        public IUnityContainer GetInstance()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IVentaRepository), typeof(VentaRepository));
            container.RegisterType(typeof(IVentaService), typeof(VentaService));
            //container.RegisterType(typeof(IVentaController), typeof(VentaController));

            container.RegisterType(typeof(ISesionRepository), typeof(SesionRepository));
            container.RegisterType(typeof(ISesionService), typeof(SesionService));
            //container.RegisterType(typeof(ISesionController), typeof(SesionController));

            container.RegisterType(typeof(IValuesController), typeof(ValuesController));

            return container;
        }
    }

}