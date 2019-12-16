using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoOrder.Models
{
    public class PizzaModel
    {
        public string PizzaName { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Energy { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }
}
