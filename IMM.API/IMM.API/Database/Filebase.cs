using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IMM.Models;

namespace IMM.API.Database
{
    public class Filebase
    {
        private string _root;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp\Items";
        }

        public int NextItemId
        {
            get
            {
                if (!Items.Any())
                {
                    return 1;
                }

                return Items.Select(i => i.Id).Max() + 1;
            }
        }

        public Item AddOrUpdate(Item item)
        {
            //set up a new Id if one doesn't already exist
            if(item.Id <= 0)
            {
                item.Id = NextItemId;
            }

            //go to the right place
            string path = $"{_root}\\{item.Id}.json";

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }

        public List<Item> Items
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var items = new List<Item>();
                foreach(var itemFile in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(itemFile.FullName));
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
                return items;
            }
        }

        public Item? Delete(int id)
        {
            //go to the right place
            string path = $"{_root}\\{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //capture the item that is being deleted
                var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(path));
                if (item != null)
                {
                    //blow it up
                    File.Delete(path);
                    return item;
                }
                else
                {
                    File.Delete(path);
                    return null;
                }
            }
            return null;
        }
    }
}
