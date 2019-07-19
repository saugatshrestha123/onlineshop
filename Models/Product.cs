using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Saugatshrestha.Models
{
    public class Product
    {
      
        public int Id { get; set; }

        [Required]
    
        public string Item { get; set; }
        
        [Required]
        public decimal Quantity { get; set; }

        public string Unit { get; set; }
       
        public decimal Rate { get; set; }
        public string ImageUrl { get; set; }
    }
}
