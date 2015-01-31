using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class ValueService : Cine.IValueService
    {
        public string Read(int id)
        {
            return "Hola " + id;
        }
    }
}
