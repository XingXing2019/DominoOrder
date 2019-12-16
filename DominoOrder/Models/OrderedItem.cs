using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoOrder.Models
{
    public class OrderedItem
    {
        public PizzaModel Pizza { get; set; } = new PizzaModel();
        public int QTY { get; set; }
        public double TotalPrice { get; set; }
    }
}
