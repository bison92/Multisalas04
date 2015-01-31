using Cine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Cine.Model;

namespace WebCine.Controllers
{
    public class StatusController : ApiController, IStatusController
    {
        // GET api/status
        [HttpGet]
        public StatusDTO Get()
        {
            return new StatusDTO();
        }
    }
}