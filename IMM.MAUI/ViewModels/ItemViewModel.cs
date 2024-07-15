﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IMM.Models;
using IMM.Services;

namespace IMM.MAUI.ViewModels
{
    public class ItemViewModel
    {
        public Item? Item { get; set; }

        public int? RespectiveCartId { get; set; }

        public string? DisplayPrice
        {
            get
            {
                if (Item == null)
                {
                    return string.Empty;
                }
                return $"{Item.Price:C}";
            }
        }

        public string? DisplayBOGO
        {
            get
            {
                if (Item?.BOGO == null || Item?.BOGO == false)
                {
                    return string.Empty;
                }
                return "-BOGO!";
            }
        }

        public string? DisplayMarkdown
        {
            get
            {
                if (Item?.Markdown == null || Item?.Markdown == 0)
                {
                    return string.Empty;
                }
                return $"-{Item.Markdown}%";
            }
        }

        public ItemViewModel()
        {
            Item = new Item();
            SetupCommands();
        }

        public ItemViewModel(Item i)
        {
            Item = i;
            SetupCommands();
        }
        public ItemViewModel(int cartId, Item i)
        {
            Item = i;
            RespectiveCartId = cartId;
            SetupCommands();
        }

        public ItemViewModel(int id)
        {
            Item = InventoryServiceProxy.Current?.Items?.FirstOrDefault(i => i.Id == id);
            if (Item == null)
            {
                Item = new Item();
            }
            SetupCommands();
        }
        public ICommand? AddToCartCommand { get; private set; }
        public ICommand? RemoveFromCartCommand { get; private set; }
        public ICommand? EditCommand { get; private set; }
        public ICommand? DeleteCommand { get; private set; }

        public void SetupCommands()
        {
            AddToCartCommand = new Command((i) => ExecuteAddToCart(RespectiveCartId, i as ItemViewModel));
            RemoveFromCartCommand = new Command((i) => ExecuteRemoveFromCart(RespectiveCartId, i as ItemViewModel));
            EditCommand = new Command((i) => ExecuteEdit(i as ItemViewModel));
            DeleteCommand = new Command((i) => ExecuteDelete((i as ItemViewModel)?.Item?.Id));
        }

        private void ExecuteEdit(ItemViewModel? i)
        {
            if (i?.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Item?itemId={i.Item.Id}");
        }

        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }
            InventoryServiceProxy.Current.Delete(id ?? 0);
        }

        private void ExecuteAddToCart(int? cartId, ItemViewModel? i)
        {
            if (i?.Item == null)
            {
                return;
            }
            var cartItem = new Item(i.Item);
            cartItem.Stock = 1;
            CartServiceProxy.Current.AddToCart(cartId, cartItem);
        }

        private void ExecuteRemoveFromCart(int? cartId, ItemViewModel? i)
        {
            if (i?.Item == null)
            {
                return;
            }
            CartServiceProxy.Current.DeleteItem(cartId, i.Item.Id);
        }

        public void AddOrUpdate()
        {
            if (Item == null)
            {
                return;
            }
            InventoryServiceProxy.Current.AddOrUpdate(Item);
        }
    }
}
