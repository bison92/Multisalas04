using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class SesionRepository : ISesionRepository
    {

        private Sesion ReadSesion(long idSesion)
        {
            Sesion sesion = null;
            using (var context = new DatosDB())
            {
                sesion = context.Sesiones.Where(s => s.SesionId == idSesion).FirstOrDefault();
            }
            return sesion;
        }
        public SesionDTO ReadDTO(long id)
        {
            Sesion sesion = ReadSesion(id);
            return SesionToSesionDTO(sesion);

        }

        public SesionDTO Abrir(long id)
        {
            Sesion s = ReadSesion(id);

            if (!s.Abierto)
            {

                using (var context = new DatosDB())
                {
                    s = context.Sesiones.Find(id);
                    s.Abierto = true;

                    context.SaveChanges();
                }
            }
            return SesionToSesionDTO(s);
        }
        public SesionDTO Cerrar(long id)
        {
            Sesion s = ReadSesion(id);

            if (s.Abierto)
            {
                using (var context = new DatosDB())
                {
                    s = context.Sesiones.Find(id);
                    s.Abierto = false;

                    context.SaveChanges();
                }
            }
            return SesionToSesionDTO(s);
        }




        public IList<SesionDTO> List()
        {
            IList<SesionDTO> resultado = new List<SesionDTO>();

            using (var context = new DatosDB())
            {
                foreach (Sesion sesion in context.Sesiones.ToList<Sesion>())
                {
                    resultado.Add(SesionToSesionDTO(sesion));
                }
            }

            return resultado;
        }

        private SesionDTO SesionToSesionDTO(Sesion sesion)
        {
            SesionDTO resultado = new SesionDTO();
            resultado.SesionId = sesion.SesionId;
            resultado.SalaId = sesion.SalaId;
            resultado.fecha = sesion.fecha;
            resultado.Abierto = sesion.Abierto;
            return resultado;
        }


    }
}

