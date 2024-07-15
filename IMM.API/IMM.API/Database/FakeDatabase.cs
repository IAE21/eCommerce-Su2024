using IMM.Models;

namespace IMM.API.Database
{
    public static class FakeDatabase
    {
        public static IEnumerable<Item> Items
        {
            get
            {
                return new List<Item>()
                {
                    new Item { Id = 1, Name = "Lightbulbs", Description = "These are really good lightbulbs.", Price = 30, Stock = 50 },
                    new Item { Id = 2, Name = "Toothbrushes", Description = "These are really good toothbrushes.", Price = 10, Stock = 55 }
                };
            }
        }
    }
}
