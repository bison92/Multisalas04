using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class MultiSalaDBInitializer : CreateDatabaseIfNotExists<DatosDB>
    {
        protected override void Seed(DatosDB context)
        {
            IList<Sala> defaultStandards = new List<Sala>();

            defaultStandards.Add(new Sala() { SalaId = 1, Filas = 10, Columnas = 10});
            defaultStandards.Add(new Sala() { SalaId = 2, Filas = 5, Columnas = 10 });
            defaultStandards.Add(new Sala() { SalaId = 3, Filas = 10 , Columnas = 2 });

            foreach (Sala std in defaultStandards)
                context.Salas.Add(std);

            IList<Sesion> defaultSesions = new List<Sesion>();

            defaultSesions.Add(new Sesion() { SesionId = 1, Abierto = true, fecha = new DateTime(2015, 5, 12, 17, 30, 0),SalaId = 1 });
            defaultSesions.Add(new Sesion() { SesionId = 2, Abierto = false, fecha = new DateTime(2015, 5, 12, 19, 30, 0),SalaId = 1 });
            defaultSesions.Add(new Sesion() { SesionId = 3, Abierto = false, fecha = new DateTime(2015, 5, 12, 22, 00, 0),SalaId= 1 });
            defaultSesions.Add(new Sesion() { SesionId = 4, Abierto = false, fecha = new DateTime(2015, 5, 12, 17, 00, 0),SalaId= 2 });
            defaultSesions.Add(new Sesion() { SesionId = 5, Abierto = false, fecha = new DateTime(2015, 5, 12, 19, 00, 0),SalaId= 2 });
            defaultSesions.Add(new Sesion() { SesionId = 6, Abierto = false, fecha = new DateTime(2015, 5, 12, 22, 00, 0),SalaId= 2 });
            defaultSesions.Add(new Sesion() { SesionId = 7, Abierto = false, fecha = new DateTime(2015, 5, 12, 17, 00, 0),SalaId= 3 });
            defaultSesions.Add(new Sesion() { SesionId = 8, Abierto = false, fecha = new DateTime(2015, 5, 12, 19, 30, 0),SalaId= 3 });
            defaultSesions.Add(new Sesion() { SesionId = 9, Abierto = false, fecha = new DateTime(2015, 5, 12, 22, 30, 0),SalaId= 3 });
                                                                                                                        
            defaultSesions.Add(new Sesion() { SesionId = 10, Abierto = false, fecha = new DateTime(2015, 5, 13, 17, 30, 0),SalaId = 1 });
            defaultSesions.Add(new Sesion() { SesionId = 11, Abierto = false, fecha = new DateTime(2015, 5, 13, 19, 30, 0),SalaId = 1 });
            defaultSesions.Add(new Sesion() { SesionId = 12, Abierto = false, fecha = new DateTime(2015, 5, 14, 01, 00, 0),SalaId = 1 });
            defaultSesions.Add(new Sesion() { SesionId = 13, Abierto = false, fecha = new DateTime(2015, 5, 13, 17, 00, 0),SalaId = 2 });
            defaultSesions.Add(new Sesion() { SesionId = 14, Abierto = false, fecha = new DateTime(2015, 5, 13, 19, 00, 0),SalaId = 2 });
            defaultSesions.Add(new Sesion() { SesionId = 15, Abierto = false, fecha = new DateTime(2015, 5, 13, 22, 00, 0),SalaId = 2 });
            defaultSesions.Add(new Sesion() { SesionId = 16, Abierto = false, fecha = new DateTime(2015, 5, 13, 17, 00, 0),SalaId = 3 });
            defaultSesions.Add(new Sesion() { SesionId = 17, Abierto = false, fecha = new DateTime(2015, 5, 13, 19, 30, 0),SalaId = 3 });
            defaultSesions.Add(new Sesion() { SesionId = 18, Abierto = false, fecha = new DateTime(2015, 5, 13, 22, 30, 0),SalaId = 3 }); ;

            foreach (Sesion std in defaultSesions)
                context.Sesiones.Add(std);

            base.Seed(context);
        }
    }
}