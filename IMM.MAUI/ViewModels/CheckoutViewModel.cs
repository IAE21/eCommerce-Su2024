using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.Models;
using IMM.MAUI.ViewModels;
using IMM.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IMM.MAUI.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                if (Cart.Cart?.Contents == null || Cart.Cart?.Contents.Count == 0)
                {
                    return 0;
                }

                decimal total = 0;
                Cart.Contents.ForEach(i => total += ItemTotal(i));
                return total;
            }
        }

        private double taxRate
        {
            get { return InventoryServiceProxy.Current.TaxRate; }
        }

        private double tax
        {
            get { return decimal.ToDouble(subtotal) * taxRate; }
        }

        private decimal total
        {
            get { return (decimal)(decimal.ToDouble(subtotal) + tax); }
        }
        public string? Subtotal
        {
            get { return $"{subtotal:C}"; }
        }

        public string? Tax
        {
            get { return $"{tax:C}"; }
        }

        public string? Total
        {
            get { return $"{total:C}"; }
        }

        public string? DisplayTaxRate
        {
            get { return $"Tax {(taxRate * 100).ToString("0.##")}%:"; }
        }
        public CheckoutViewModel()
        {

        }

        public CheckoutViewModel(int id)
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

        public void RefreshCart()
        {
            NotifyPropertyChanged("Cart");
            NotifyPropertyChanged("Subtotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");
        }
    }
}
