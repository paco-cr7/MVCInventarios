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
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //Aqui se le dara el nombre personalizado a las tablas en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Departamento>().ToTable("Departamento");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Perfil>().ToTable("Perfil");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");

            base.OnModelCreating(modelBuilder);
        }
    }
}
