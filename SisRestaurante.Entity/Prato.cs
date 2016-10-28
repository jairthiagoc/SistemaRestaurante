using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRestaurante.Entity
{
    
    public class Prato
    {
        public int PratoId { get; set; }
        public string Nome { get; set; }
        public Decimal Preco  { get; set; }
        public int RestauranteId { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}
