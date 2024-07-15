using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MAUI.ViewModels
{
    public class MainPageViewModel
    {
        private string welcome;
        public string Welcome
        {
            get { return welcome; }
            set { welcome = value; }
        }

        private string inventory;
        public string Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        private string shop;
        public string Shop
        {
            get { return shop; }
            set { shop = value; }
        }
        public MainPageViewModel() 
        {
            Welcome = "Welcome to Item Marketplace Management!";
            Inventory = "Inventory Management System";
            Shop = "Shop";
        }
    }
}
