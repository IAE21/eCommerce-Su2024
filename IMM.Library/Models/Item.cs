using System.Windows.Input;

namespace IMM.Models
{
    public class Item
    {
        private string? name;
        public string? Name {
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
            get { return markdown*100; }
            set { markdown = Convert.ToDouble(value)/100; }
        }

        private bool? bogo;
        public bool? BOGO
        {
            get { return bogo; }
            set { bogo = value; }
        }

        public override string ToString()
        {
            return $"ID: {Id}\nName: {Name}\nDescription: {Description}\nPrice: ${Price}\nNumber in Stock: {Stock}";
        }

        public Item()
        {

        }

        public Item(Item i)
        {
            Name = i.Name;
            Description = i.Description;
            Price = i.Price;
            Id = i.Id;
            Stock = i.Stock;
            Markdown = i.Markdown;
            BOGO = i.BOGO;
        }
    }
}
