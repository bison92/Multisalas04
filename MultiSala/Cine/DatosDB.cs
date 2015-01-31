using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class DatosDB : DbContext
    {
        public DatosDB()
        {
            Database.SetInitializer<DatosDB>(new MultiSalaDBInitializer());
        }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
    }
}