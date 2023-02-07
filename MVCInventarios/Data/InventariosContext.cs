using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCInventarios.Models;

namespace MVCInventarios.Data
{
    public class InventariosContext : DbContext
    {
        public InventariosContext (DbContextOptions<InventariosContext> options)
            : base(options)
        {
        }

        public DbSet<Departamento> Departamentos {get; set; }
        public DbSet<Marca> Marcas {get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
