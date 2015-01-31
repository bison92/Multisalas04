using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public interface ISesionService
    {
        IList<SesionDTO> List();
        SesionDTO Cerrar(long id);
        SesionDTO Abrir(long id);
        SesionDTO Read(long id);
    }
}
