using IMM.API.Database;
using IMM.Models;
using IMM.DTO;

namespace IMM.API.EC
{
    public class InventoryEC
    {
        
        public InventoryEC() 
        {

        }

        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return FakeDatabase.Items.Take(100).Select(i => new ItemDTO(i));
        }
    }
}
