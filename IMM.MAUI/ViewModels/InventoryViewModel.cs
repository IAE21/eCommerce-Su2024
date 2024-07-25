using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMM.Services;
using IMM.DTO;

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

        public async void RefreshItems()
        {
            await InventoryServiceProxy.Current.Get();
            await InventoryServiceProxy.Current.Get();
            NotifyPropertyChanged(nameof(Items));
        }

        public async void Import()
        {
            var spreadsheet = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".csv" } }, // file extension
                });

            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = spreadsheet
            });

            if (result == null)
            {
                return;
            }

            List<ItemDTO> importItems = new List<ItemDTO>();
            var file = await result.OpenReadAsync();
            using (StreamReader stream = new StreamReader(file))
            {
                string line = string.Empty;
                while ((line = stream.ReadLine()) != null)
                {
                    var tokens = line.Split(['|']);

                    importItems.Add(new ItemDTO
                    {
                        Name = tokens[0],
                        Description = tokens[1],
                        Price = decimal.Parse(tokens[2]),
                        Stock = int.Parse(tokens[3]),
                        Markdown = double.Parse(tokens[4]),
                        BOGO = bool.Parse(tokens[5]),
                    });
                }
            }
            foreach (var item in importItems)
            {
                await InventoryServiceProxy.Current.AddOrUpdate(item);
            }
            RefreshItems();
        }
    }
}
