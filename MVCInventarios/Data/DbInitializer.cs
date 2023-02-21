using Microsoft.SqlServer.Server;
using MVCInventarios.Models;

namespace MVCInventarios.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventariosContext context)
        {
            if (context.Marcas.Any())
            {
                return;
            }

            var marcas = new Marca[]
            {
                new Marca{Nombre="Rino"},
                new Marca{Nombre="Niku"},
                new Marca{Nombre="Rocco"},
                new Marca{Nombre="Razir"},
                new Marca{Nombre="Loginano"},
                new Marca{Nombre="RedDesk"},
                new Marca{Nombre="Azuri"},
                new Marca{Nombre="LeCiel"},
                new Marca{Nombre="Radomi"},
                new Marca{Nombre="Bazi"},
                new Marca{Nombre="Dall"},
                new Marca{Nombre="Asis"},
                new Marca{Nombre="Cote"},
                new Marca{Nombre="Arty"},
            };

            context.Marcas.AddRange(marcas);
            context.SaveChanges();

            var departamentos = new Departamento[]
            {
                new Departamento{
                    Nombre="Administración General",
                    Descripcion="Departamento encargado de la administración de la empresa",
                    FechaCreacion= DateTime.Now
                },
                new Departamento{
                    Nombre="Recursos Humanos",
                    Descripcion="Departamento encargado de la gestíón del personal de la empresa",
                    FechaCreacion= DateTime.Now
                },
                new Departamento{
                    Nombre="Recursos Materiales",
                    Descripcion="Departamento encargado del control del immobiliario de la empresa",
                    FechaCreacion= DateTime.Now
                },
                new Departamento{
                    Nombre="Informática",
                    Descripcion="Departamento encargado del hardware y software",
                    FechaCreacion= DateTime.Now
                },
                new Departamento{
                    Nombre="Dirección General",
                    Descripcion="Departamento encargado del control total de la empresa",
                    FechaCreacion= DateTime.Now
                },
            };

            context.Departamentos.AddRange(departamentos);
            context.SaveChanges();

            var productos = new Producto[]
            {
                new Producto
                {
                    Nombre="Silla Secretarial",
                    Descripcion="Silla de piel secretarial",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Rino").Id,
                    Costo=3500
                },
                new Producto
                {
                    Nombre="Escritorio Gerencial",
                    Descripcion="Escritorio Negro con cristak templado",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Azuri").Id,
                    Costo=6500
                },
                new Producto
                {
                    Nombre="Cafetera Industrial",
                    Descripcion="Cafetera para 50 tazas",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Rocco").Id,
                    Costo=32500
                },
                new Producto
                {
                    Nombre="Computadora",
                    Descripcion="Computadora Gamer",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Asis").Id,
                    Costo=65500
                },
                new Producto
                {
                    Nombre="Proyector",
                    Descripcion="Proyector Inalambrico",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Rino").Id,
                    Costo=6500
                },
                new Producto
                {
                    Nombre="Audífonos Gamers",
                    Descripcion="Audífonos Gamer con Cancelación de Ruido",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="LeCiel").Id,
                    Costo=32500
                },
                new Producto
                {
                    Nombre="Mezclado Audio",
                    Descripcion="Mezcladora de Audio de 2 canales",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Rocco").Id,
                    Costo=32500
                },
                new Producto
                {
                    Nombre="Monitores de Estudio",
                    Descripcion="Monitores de Estudio de 5 pulgadas",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Dall").Id,
                    Costo=12500
                },
                new Producto
                {
                    Nombre="Escritorio Gerencial de Aluminio",
                    Descripcion="Escritorio Gerencial de Aluminio y Titanio",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="RedDesk").Id,
                    Costo=33500
                },
                new Producto
                {
                    Nombre="Laptop Gamer EXT",
                    Descripcion="Laptop Gamer con i9 12va Generación",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Cote").Id,
                    Costo=12500
                },
                new Producto
                {
                    Nombre="Cámara Profesional Grabación",
                    Descripcion="Cámara Profesional de Grabación 4K",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Radomi").Id,
                    Costo=33500
                },
            };

            context.Productos.AddRange(productos);
            context.SaveChanges();
        }
    }
}
