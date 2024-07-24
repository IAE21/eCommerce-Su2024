using IMM.Models;
using IMM.Services;
using IMM.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IMM.MAUI.ViewModels
{
    public class CartViewModel
    {
        public Cart? Cart { get; set; }

        public List<ItemViewModel>? Contents
        {
            get { return Cart?.Contents?.Select(i => new ItemViewModel(Cart.Id, i))?.ToList() ?? new List<ItemViewModel>(); }
        }

        private decimal ItemTotal(ItemDTO? i)
        {
            if (i == null)
            {
                return 0;
            }

            if (i.Markdown == null || i.Markdown == 0)
            {
                if (i.BOGO == null || i.BOGO == false)
                {
                    return (decimal)(i.Price * i.Stock);
                }
                return (decimal)(i.Price * (decimal)0.5 * i.Stock);
            }

            if (i.BOGO == null || i.BOGO == false)
            {
                return (decimal)((i.Price * (decimal)(1 - (i.Markdown / 100))) * i.Stock);
            }
            return (decimal)((i.Price * (decimal)(1 - (i.Markdown / 100))) * (decimal)0.5 * i.Stock);
        }
        private decimal subtotal
        {
            get
            {
                if (Cart?.Contents == null || Cart.Contents.Count == 0)
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

        public string? DisplayCount
        {
            get
            {
                int? count = 0;
                Cart.Contents.ForEach(i => count += i.Stock);
                if (count == 1)
                {
                    return $"{count} Item";
                }
                return $"{count} Items";
            }
        }

        public int? DisplayId
        {
            get
            {
                return Cart.Id - 1;
            }
        }

        public CartViewModel()
        {
            Cart = new Cart();
            SetupCommands();
        }

        public CartViewModel(Cart c)
        {
            if (c == null)
            {
                Cart = new Cart();
            }
            else
            {
                Cart = c;
            }
            SetupCommands();
        }

        public CartViewModel(int id)
        {
            Cart = CartServiceProxy.Current.Carts.FirstOrDefault(c => c.Id == id);
            if (Cart == null)
            {
                Cart = new Cart();
            }
            SetupCommands();
        }

        public ICommand? ShopCommand { get; private set; }
        public ICommand? EditCommand { get; private set; }
        public ICommand? DeleteCommand { get; private set; }

        public void SetupCommands()
        {
            ShopCommand = new Command((c) => ExecuteShop(c as CartViewModel));
            EditCommand = new Command((c) => ExecuteEdit(c as CartViewModel));
            DeleteCommand = new Command((c) => ExecuteDelete((c as CartViewModel)?.Cart?.Id));
        }

        private void ExecuteShop(CartViewModel? c)
        {
            if (c?.Cart == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Shop?cartId={c.Cart.Id}");
        }

        private void ExecuteEdit(CartViewModel? c)
        {
            if (c?.Cart == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Wishlist?cartId={c.Cart.Id}");
        }

        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }
            CartServiceProxy.Current.DeleteCart(id ?? 0, false);
        }

        public void AddOrUpdate()
        {
            if (Cart == null)
            {
                return;
            }
            CartServiceProxy.Current.AddOrUpdateCart(Cart);
        }
    }
}
