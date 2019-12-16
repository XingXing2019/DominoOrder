using DominoOrder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DominoOrder.Services
{
    public class XmlDataAccess
    {
        public static List<PizzaModel> GetPizzaMenu()
        {
            var pizzaMenu = new List<PizzaModel>();
            string xmlFilePath = @"..\..\Services\PizzaMenu.xml";
            var xDoc = XDocument.Load(xmlFilePath);
            var pizzas = xDoc.Descendants("PizzaModel");
            foreach (var p in pizzas)
            {
                var pizza = new PizzaModel();
                pizza.PizzaName = p.Element("PizzaName").Value;
                pizza.Description = p.Element("Description").Value;
                pizza.SmallImagePath = p.Element("SmallImagePath").Value;
                pizza.LargeImagePath = p.Element("LargeImagePath").Value;
                pizza.Energy = int.Parse(p.Element("Energy").Value);
                pizza.Price = double.Parse(p.Element("Price").Value);
                pizzaMenu.Add(pizza);
            }
            return pizzaMenu;
        }
        public static List<ToppingModel> GetToppingList()
        {
            var toppingList = new List<ToppingModel>();
            string xmlFilePath = @"..\..\Services\ToppingList.xml";
            var xDoc = XDocument.Load(xmlFilePath);
            var toppings = xDoc.Descendants("ToppingModel");
            foreach (var t in toppings)
            {
                var topping = new ToppingModel();
                topping.ToppingName = t.Element("ToppingName").Value;
                topping.ImagePath = t.Element("ImagePath").Value;
                topping.Price = double.Parse(t.Element("Price").Value);
                toppingList.Add(topping);
            }
            return toppingList;
        }
    }
}
