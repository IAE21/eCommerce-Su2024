using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.Models;
using IMM.Utilities;
using IMM.DTO;
using Newtonsoft.Json;
namespace IMM.Services
{
    public class InventoryServiceProxy
    {
        private List<ItemDTO> items;

        private double taxRate;

        private InventoryServiceProxy() 
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            items = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
            taxRate = 0.07;
        }

        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }

                return instance;
            }
        }

        //CRUD Functionality---------------------------

        //Read
        public ReadOnlyCollection<ItemDTO> Items
        {
            get { return items.AsReadOnly(); }
        }

        public async Task<IEnumerable<ItemDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var refreshedList = JsonConvert.DeserializeObject<List<ItemDTO>>(result);
            items = refreshedList?.ToList() ?? new List<ItemDTO>();
            return items;
        }

        public double TaxRate
        {
            get { return taxRate; }
        }

        //Add & Update
        public async Task<ItemDTO> AddOrUpdate(ItemDTO item)
        {
            var result = await new WebRequestHandler().Post("/Inventory", item);
            return JsonConvert.DeserializeObject<ItemDTO>(result);
        }

        public void UpdateTaxRate(double? t)
        {
            if (t == null)
            {
                return;
            }

            taxRate = t.Value;
        }

        //Delete
        public void Delete(int id)
        {
            if (items == null)
            {
                return;
            }

            var deleteItem = items.FirstOrDefault(c => c.Id == id);

            if (deleteItem != null)
            {
                items.Remove(deleteItem);
            }
        }

    }
}
