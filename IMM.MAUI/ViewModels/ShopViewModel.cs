using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IMM.Models;
using IMM.Services;

namespace IMM.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ItemViewModel> Items
        {
            get { return InventoryServiceProxy.Current.Items.Select(i => new ItemViewModel(Cart.Cart.Id, i)).ToList(); }
        }

        public CartViewModel Cart { get; set; }

        private decimal ItemTotal(ItemViewModel? i)
        {
            if (i?.Item == null)
            {
                return 0;
            }

            if (i.Item.Markdown == null || i.Item.Markdown == 0)
            {
                if (i.Item.BOGO == null || i.Item.BOGO == false)
                {
                    return (decimal)(i.Item.Price * i.Item.Stock);
                }
                return (decimal)(i.Item.Price * (decimal)0.5 * i.Item.Stock);
            }

            if (i.Item.BOGO == null || i.Item.BOGO == false)
            {
                return (decimal)((i.Item.Price * (decimal)(1 - (i.Item.Markdown / 100))) * i.Item.Stock);
            }
            return (decimal)((i.Item.Price * (decimal)(1 - (i.Item.Markdown / 100))) * (decimal)0.5 * i.Item.Stock);
        }
        private decimal subtotal
        {
            get
            {
                if (Cart.Cart?.Contents == null || Cart.Cart?.Contents.Count == 0 )
                {
                    return 0;
                }

                decimal total = 0;
                Cart.Contents.ForEach(i => total += ItemTotal(i));
                return total;
            }
        }

        public string? Subtotal
        {
            get { return $"{subtotal:C}"; }
        }

        public string? CartDisplayName
        {
            get 
            { 
                if (Cart.Cart?.Name == null || Cart.Cart.Id == 1)
                {
                    return "Cart:";
                }
                return $"{Cart.Cart.Name} Wishlist:"; 
            }
        }

        public ShopViewModel() 
        {

        }

        public ShopViewModel(int id)
        {
            var existCart = CartServiceProxy.Current.Carts.FirstOrDefault(c => c.Id == id);
            if (existCart == null)
            {
                Cart = new CartViewModel();
            }
            else
            {
                Cart = new CartViewModel(existCart);
            }
        }

        public void RefreshShop()
        {
            NotifyPropertyChanged("Items");
            NotifyPropertyChanged("Cart");
            NotifyPropertyChanged("Subtotal");
        }
    }
}
