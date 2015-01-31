using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class SesionService : ISesionService
    {
        public ISesionRepository SesionRepository { get; set; }

        public SesionService(ISesionRepository sesionRepository)
        {
            this.SesionRepository = sesionRepository;
        }
        public IList<SesionDTO> List()
        {
            return SesionRepository.List();
        }

        public SesionDTO Cerrar(long id)
        {
            return SesionRepository.Cerrar(id);
        }
        public SesionDTO Read(long id)
        {
            return SesionRepository.ReadDTO(id);
        }
        public SesionDTO Abrir(long id)
        {
            return SesionRepository.Abrir(id);
        }
    }
}
