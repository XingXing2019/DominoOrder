using DominoOrder.Models;
using DominoOrder.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DominoOrder.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private PizzaModel _selectedPizza;
        private double _price;
        private ObservableCollection<OrderedItem> _orderedPizza;
        private double _totalPrice;
        private int _qty;
        #endregion

        #region Properties
        public List<PizzaModel> PizzaMenu { get; set; }
        public List<ToppingModel> ToppingMenu { get; set; }
        public PizzaModel SelectedPizza
        {
            get { return _selectedPizza; }
            set
            {
                _selectedPizza = value;
                this.RaisePropertyChanged("SelectedPizza");
            }
        }
        public ObservableCollection<ToppingModel> Toppings { get; set; }
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                this.RaisePropertyChanged("Price");
            }
        }
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                this.RaisePropertyChanged("TotalPrice");
            }
        }
        public int QTY
        {
            get { return _qty; }
            set
            {
                _qty = value;
                this.RaisePropertyChanged("QTY");
            }
        }
        public ObservableCollection<OrderedItem> OrderedItems
        {
            get { return _orderedPizza; }
            set
            {
                _orderedPizza = value;
                this.RaisePropertyChanged("OrderedPizza");
            }
        }
        #endregion

        #region Commands
        public RelayCommand<PizzaModel> SelectPizzaCommand { get; set; }
        public RelayCommand<ToppingModel> SelectToppingCommand { get; set; }
        public RelayCommand<ToppingModel> RemoveToppingCommand { get; set; }
        public RelayCommand<PizzaModel> PlaceOrderCommand { get; set; }
        public RelayCommand<OrderedItem> RemovePizzaCommand { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            PizzaMenu = XmlDataAccess.GetPizzaMenu();
            ToppingMenu = XmlDataAccess.GetToppingList();
            Toppings = new ObservableCollection<ToppingModel>();
            OrderedItems = new ObservableCollection<OrderedItem>();          
            
            SelectPizzaCommand = new RelayCommand<PizzaModel>(p => SelectPizzaCommandExecute(p));
            SelectToppingCommand = new RelayCommand<ToppingModel>(t => SelectToppingCommandExecute(t));
            RemoveToppingCommand = new RelayCommand<ToppingModel>(t => RemoveToppingCommandExecute(t));
            PlaceOrderCommand = new RelayCommand<PizzaModel>(p => PlaceOrderCommandExecute(p));
            RemovePizzaCommand = new RelayCommand<OrderedItem>(p => RemovePizzaCommandExecute(p));
        }
        #endregion
        
        #region CommandExecute
        private void SelectPizzaCommandExecute(PizzaModel pizza)
        {
            SelectedPizza = pizza;
            Price = SelectedPizza.Price;
            QTY = 1;
            Messenger.Default.Send(pizza, "Expand");
        }
        private void SelectToppingCommandExecute(ToppingModel topping)
        {
            if (Toppings.Count >= 7)
                return;
            if (Toppings.Any(t => t.ToppingName == topping.ToppingName))
                return;
            Toppings.Add(topping);
            Price += topping.Price;
        }
        private void RemoveToppingCommandExecute(ToppingModel topping)
        {
            Toppings.Remove(topping);
            Price -= topping.Price;
        }
        private void PlaceOrderCommandExecute(PizzaModel pizza)
        {
            Toppings.Clear();
            var orderedItem = new OrderedItem();
            orderedItem.Pizza.PizzaName = SelectedPizza.PizzaName;
            orderedItem.Pizza.Energy = SelectedPizza.Energy;
            orderedItem.TotalPrice = Price * QTY;
            orderedItem.QTY = QTY;
            TotalPrice += orderedItem.TotalPrice;
            OrderedItems.Add(orderedItem);
            Messenger.Default.Send(pizza, "Expand");
        }
        private void RemovePizzaCommandExecute(OrderedItem orderedItem)
        {
            OrderedItems.Remove(orderedItem);
            TotalPrice -= orderedItem.TotalPrice;
            TotalPrice = Math.Round(TotalPrice, 2);
        }
        #endregion
    }
}