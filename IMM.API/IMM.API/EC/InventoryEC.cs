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
            return Filebase.Current.Items.Take(100).Select(i => new ItemDTO(i));
        }

        public async Task<ItemDTO> AddOrUpdate(ItemDTO i)
        {
            return new ItemDTO(Filebase.Current.AddOrUpdate(new Item(i)));
        }

        public async Task<ItemDTO?> Delete(int id)
        {
            var deleteItem = Filebase.Current.Delete(id);
            if (deleteItem != null)
            {
                return new ItemDTO(deleteItem);
            }
            return null;
        }
    }
}
