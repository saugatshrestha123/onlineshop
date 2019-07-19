using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saugatshrestha.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Rate { get; set; }
        public string Image { get; set; }
    }
}
