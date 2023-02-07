﻿using MVCInventarios.Models;

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
                new Marca{Nombre="RedDsk"},
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
                new Departamento{Nombre="Administración General"},
                new Departamento{Nombre="Recursos Humanos"},
                new Departamento{Nombre="Recursos Materiales"},
                new Departamento{Nombre="Informática"},
                new Departamento{Nombre="Dirección General"},
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
                    Costo=3500M
                },
                new Producto
                {
                    Nombre="Escritorio Gerencial",
                    Descripcion="Escritorio Negro con cristak templado",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Azuri").Id,
                    Costo=6500M
                },
                new Producto
                {
                    Nombre="Cafetera Industrial",
                    Descripcion="Cafetera para 50 tazas",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Rocco").Id,
                    Costo=32500M
                },
                new Producto
                {
                    Nombre="Computadora",
                    Descripcion="Computadora Gamer",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Asis").Id,
                    Costo=65500M
                },
                new Producto
                {
                    Nombre="Proyector",
                    Descripcion="Proyector Inalambrico",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Reni").Id,
                    Costo=6500M
                },
                new Producto
                {
                    Nombre="Audífonos Gamers",
                    Descripcion="Audífonos Gamer con Cancelación de Ruido",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="LeCiel").Id,
                    Costo=32500M
                },
                new Producto
                {
                    Nombre="Mezclado Audio",
                    Descripcion="Mezcladora de Audio de 2 canales",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Rocco").Id,
                    Costo=32500M
                },
                new Producto
                {
                    Nombre="Monitores de Estudio",
                    Descripcion="Monitores de Estudio de 5 pulgadas",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Dall").Id,
                    Costo=12500M
                },
                new Producto
                {
                    Nombre="Escritorio Gerencial de Aluminio",
                    Descripcion="Escritorio Gerencial de Aluminio y Titanio",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="RedDesk").Id,
                    Costo=33500M
                },
                new Producto
                {
                    Nombre="Laptop Gamer EXT",
                    Descripcion="Laptop Gamer con i9 12va Generación",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Cote").Id,
                    Costo=12500M
                },
                new Producto
                {
                    Nombre="Cámara Profesional Grabación",
                    Descripcion="Cámara Profesional de Grabación 4K",
                    MarcaId=context.Marcas.First(u=>u.Nombre=="Randomi").Id,
                    Costo=33500M
                },
            };
            context.Productos.AddRange(productos);
            context.SaveChanges();
        }
    }
}
