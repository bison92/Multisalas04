using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cine;

namespace WebCine.Controllers
{
    public interface ISesionController
    {
        SesionDTO Get(int id);
        IList<SesionDTO> Get();

        SesionDTO GetCerrar(int id);
        SesionDTO GetAbrir(int id);
        SesionDTO Cerrar(long id);
        SesionDTO Post(long id);
        SesionDTO Abrir(long id);
    }
}
