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

        public double TaxRate
        {
            get { return taxRate; }
        }

        //Automatically increments ID
        private int NextId
        {
            get
            {
                if (!items.Any())
                {
                    return 1;
                }

                return items.Select(i => i.Id).Max() + 1;
            }
        }

        //Add & Update
        public ItemDTO? AddOrUpdate(ItemDTO? item)
        {
            if (items == null || item == null)
            {
                return null;
            }

            bool add = false;
            if (item.Id == 0)
            {
                item.Id = NextId;
                add = true;
            }

            if (add)
            {
                items.Add(item);
            }

            return item;
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
