using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMM.Models;
using IMM.Services;

namespace IMM.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ItemViewModel> Items
        {
            get 
            { 
                return InventoryServiceProxy.Current?.Items?.Where(i => i != null).Select(i => new ItemViewModel(i)).ToList() ?? new List<ItemViewModel>(); 
            }
        }

        public ItemViewModel SelectedItem { get; set; }

        public InventoryViewModel() 
        {

        }

        public void RefreshItems()
        {
            NotifyPropertyChanged("Items");
        }
    }
}
