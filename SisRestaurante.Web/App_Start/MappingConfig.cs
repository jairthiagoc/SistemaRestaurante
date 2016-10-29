using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisRestaurante.Entity;
using SisRestaurante.Web.Models;

namespace SisRestaurante.Web.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMap()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Restaurante, RestauranteViewModel>();
            });
        }
    }
}