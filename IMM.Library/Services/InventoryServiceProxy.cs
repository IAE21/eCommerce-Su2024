using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.Models;
using IMM.Utilities;
using Newtonsoft.Json;
namespace IMM.Services
{
    public class InventoryServiceProxy
    {
        private List<Item> items;

        private double taxRate;

        private InventoryServiceProxy() 
        {
            //items = new List<Item>()
            //{
            //    new Item 
            //    {
            //        Id = 1, 
            //        Name = "Lightbulbs", 
            //        Description = "These are really good lightbulbs.",
            //        Price = 30,
            //        Stock = 50
            //    },
            //    new Item 
            //    {
            //        Id = 2, 
            //        Name = "Toothbrushes", 
            //        Description = "These are really good toothbrushes.",
            //        Price = 10,
            //        Stock = 55
            //    }
            //};
            var response = new WebRequestHandler().Get("/Inventory").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(response);
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
        public ReadOnlyCollection<Item> Items
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
        public Item? AddOrUpdate(Item? item)
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
