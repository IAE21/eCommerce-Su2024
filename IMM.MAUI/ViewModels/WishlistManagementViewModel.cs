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
    public class WishlistManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<CartViewModel>? Carts 
        { 
            get { return CartServiceProxy.Current.Carts.Where(c => c != null && c.Id != 1).Select(c => new CartViewModel(c)).ToList(); }
        }

        public void RefreshWishlists()
        {
            NotifyPropertyChanged("Carts");
        }
    }
}
