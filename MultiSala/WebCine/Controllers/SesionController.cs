using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using Cine;



namespace WebCine.Controllers
{
    public class SesionController : ApiController, ISesionController
    {

        public ISesionService SesionServicio { get; set; }

        public SesionController (ISesionService sesionServicio)
        {
            this.SesionServicio = sesionServicio;
        }

        // GET api/sesion
        public SesionDTO Get(int id)
        {
            return SesionServicio.Read(id);
        }

        public IList<SesionDTO> Get()
        {
            return SesionServicio.List();
        }
        [HttpPut]
        [Route("~/api/sesion/{id}/cerrar")]

        public SesionDTO GetCerrar(int id)
        {
            SesionDTO sesion = null;
            sesion = this.Cerrar(id);
            return sesion;
        }

        // GET api/cerrar/sesion/id
        [HttpPut]
        [Route("~/api/sesion/{id}/abrir")]

        public SesionDTO GetAbrir(int id)
        {
            SesionDTO sesion = null;
            sesion = this.Abrir(id);
            return sesion;
        }


        public SesionDTO Post(long id)
        {
            return SesionServicio.Abrir(id);
        }
        public SesionDTO Cerrar(long id)
        {
            return SesionServicio.Cerrar(id);
        }
        public SesionDTO Abrir(long id)
        {
            return SesionServicio.Abrir(id);
        }



    }
}
