using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.Models;
using IMM.DTO;

namespace IMM.Services
{
    public class CartServiceProxy
    {
        private List<Cart> carts;

        private CartServiceProxy()
        {
            carts = new List<Cart>() 
            { 
                new Cart() 
                { 
                    Id = 1,
                } 
            };
        }

        public Cart Cart
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    return new Cart();
                }
                return carts?.FirstOrDefault() ?? new Cart();
            }
        }

        private static CartServiceProxy? instance;
        private static object instanceLock = new object();

        public static CartServiceProxy Current 
        { 
            get 
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartServiceProxy();
                    }
                }

                return instance; 
            }
        }

        //CRUD Functionality-----------------------------

        //Read
        public ReadOnlyCollection<Cart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        //Automatically increments ID
        private int NextId
        {
            get
            {
                if (!carts.Any())
                {
                    return 1;
                }

                return carts.Select(c => c.Id).Max() + 1;
            }
        }

        //Add & Update in the Cart
        public void AddToCart(int? cartId, ItemDTO item)
        {
            if (carts.Count == 0)
            {
                return;
            }

            var existCart = Carts.FirstOrDefault(c => c.Id == cartId);

            if (existCart == null)
            {
                return;
            }

            var existItem = existCart.Contents.FirstOrDefault(cartItems => cartItems.Id == item.Id);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(invItem => invItem.Id == item.Id);

            if (inventoryItem == null)
            {
                return;
            }
            else if (item.Stock > inventoryItem.Stock)
            {
                return;
            }

            inventoryItem.Stock -= item.Stock;

            if (existItem != null)
            {
                existItem.Stock += item.Stock;
            }
            else
            {
                existCart.Contents.Add(item);
            }
        }

        //Add & Update List of Carts
        public Cart? AddOrUpdateCart(Cart cart)
        {
            if (carts == null)
            {
                return null;
            }

            bool add = false;
            if (cart.Id == 0)
            {
                cart.Id = NextId;
                add = true;
            }

            if (add)
            {
                carts.Add(cart);
            }

            return cart;
        }

        //Delete
        public void DeleteItem(int? cartId, int itemId)
        {
            if (carts.Count == 0)
            {
                return;
            }

            var existCart = Carts.FirstOrDefault(c => c.Id == cartId);

            if (existCart == null)
            {
                return;
            }

            var deleteItem = existCart.Contents.FirstOrDefault(c => c.Id == itemId);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(c => c.Id == itemId);

            if (inventoryItem == null)
            {
                return;
            }

            if (deleteItem != null)
            {
                inventoryItem.Stock += deleteItem.Stock;
                existCart.Contents.Remove(deleteItem);
            }
        }

        public void DeleteCart(int id)
        {
            if (!carts.Any())
            {
                return;
            }

            var deleteCart = Carts.FirstOrDefault(c => c.Id == id);

            if (deleteCart == null)
            {
                return;
            }

            carts.Remove(deleteCart);
        }
    }
}
