using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace SisRestaurante.Web.Models
{
    [Table("Prato")]
    public class PratoViewModel
    {
        public int PratoId { get; set; }
        [Display(Name = "Prato")]
        
        [Required(ErrorMessage = "O campo prato tem que ser preenchido!")]
        [MinLength(5, ErrorMessage = "O campo restaurante tem que ter no minímo 5 e no maximo 50 caracteres"), MaxLength(50)]
        public string Nome { get; set; }
        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo preço tem que ser preenchido!")]
        public Decimal Preco { get; set; }
        public int RestauranteId { get; set; }
        public RestauranteViewModel Restaurante { get; set; }
    }
}