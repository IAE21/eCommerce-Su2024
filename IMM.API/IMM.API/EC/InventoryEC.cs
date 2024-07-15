using IMM.API.Database;
using IMM.Models;

namespace IMM.API.EC
{
    public class InventoryEC
    {
        
        public InventoryEC() 
        {

        }

        public async Task<IEnumerable<Item>> Get()
        {
            return FakeDatabase.Items.Take(100);
        }
    }
}
