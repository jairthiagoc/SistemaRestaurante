﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SisRestaurante.Web.Models
{
    [Table("Restaurante")]
    public class RestauranteView
    {
        public int RestauranteId { get; set; }

        [Display(Name = "Restaurante")]
        [MinLength(5 , ErrorMessage = "O campo restaurante tem que ter no minímo 5 e no maximo 50 caracteres"),MaxLength(50)]
        [Required(ErrorMessage = "O campo restaurante tem que ser preenchido!")]
        public string Nome { get; set; }

        public ICollection<PratoView> Pratos { get; set; }
    }
}