using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRestaurante.Entity
{
    
    public class Restaurante
    {
        public int RestauranteId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Prato> Pratos { get; set; }
    }
}
