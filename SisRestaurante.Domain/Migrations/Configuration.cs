using System.Collections.Generic;
using SisRestaurante.Entity;

namespace SisRestaurante.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SisRestaurante.Domain.Context.BancoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SisRestaurante.Domain.Context.BancoContext context)
        {
            Restaurante viena = new Restaurante {
                Nome = "Viena"};
            Restaurante terraco = new Restaurante { Nome = "Terraço" };
            Restaurante laPizza = new Restaurante { Nome = "La Pizza" };

            Prato estrogonofre = new Prato()
            {
                Nome = "Strogonoffe", RestauranteId = viena.RestauranteId, Preco = Convert.ToDecimal("24.80")
            };
            context.Restaurantes.AddOrUpdate(viena);
            context.Pratos.AddOrUpdate(estrogonofre);
            
            Prato Pizza = new Prato()
            {
                Nome = "pizza mussarela",
                RestauranteId = laPizza.RestauranteId,
                Preco = Convert.ToDecimal("35.50")
            };
            context.Restaurantes.AddOrUpdate(laPizza);
            context.Pratos.AddOrUpdate(Pizza);
           
            Prato CarnedeSol = new Prato()
            {
                Nome = "Carne de Sol",
                RestauranteId = terraco.RestauranteId,
                Preco = Convert.ToDecimal("49.90")
            };
            context.Restaurantes.AddOrUpdate(terraco);
            context.Pratos.AddOrUpdate(CarnedeSol);
        }
    }
}
