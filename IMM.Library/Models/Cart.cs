using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.Models
{
    public class Cart
    {
        private int id;
        public int Id 
        {
            get { return id; }
            set { id = value; } 
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        public int? Count
        {
            get 
            { 
                if (Contents == null)
                {
                    return 0;
                }
                return Contents.Count; 
            }
        }

        public List<Item> Contents { get; set; }

        public Cart() 
        {
            Contents = new List<Item>();
        }


    }
}
