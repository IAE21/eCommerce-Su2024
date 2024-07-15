using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.Models;

namespace IMM.DTO
{
    public class ItemDTO
    {
        private string? name;
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set { description = value; }
        }

        private decimal? price;
        public decimal? Price
        {
            get { return price; }
            set { price = value; }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int? stock;
        public int? Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        private double? markdown;
        public double? Markdown
        {
            get { return markdown * 100; }
            set { markdown = Convert.ToDouble(value) / 100; }
        }

        private bool? bogo;
        public bool? BOGO
        {
            get { return bogo; }
            set { bogo = value; }
        }

        public ItemDTO()
        {

        }

        public ItemDTO(Item i)
        {
            Name = i.Name;
            Description = i.Description;
            Price = i.Price;
            Stock = i.Stock;
            Markdown = i.Markdown;
            BOGO = i.BOGO;
        }
    }
}
