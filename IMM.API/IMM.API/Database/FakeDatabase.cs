using IMM.Models;

namespace IMM.API.Database
{
    public static class FakeDatabase
    {
        //Automatically increments ID
        public static int NextItemId
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

        public static List<Item> Items { get; } = new List<Item>()
        {
            new Item { Id = 1, Name = "Lightbulbs", Description = "These are really good lightbulbs.", Price = 30, Stock = 50 },
            new Item { Id = 2, Name = "Toothbrushes", Description = "These are really good toothbrushes.", Price = 10, Stock = 55 }
        };
    }
}
