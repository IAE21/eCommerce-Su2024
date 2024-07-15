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

        public async Task<ItemDTO> AddOrUpdate(ItemDTO i)
        {
            bool add = false;
            if (i.Id == 0)
            {
                i.Id = FakeDatabase.NextItemId;
                add = true;
            }

            if (add)
            {
                FakeDatabase.Items.Add(new Item(i));
            }
            else
            {
                var updateItem = FakeDatabase.Items.FirstOrDefault(item => item.Id == i.Id);
                if (updateItem != null)
                {
                    var index = FakeDatabase.Items.IndexOf(updateItem);
                    FakeDatabase.Items.RemoveAt(index);
                    updateItem = new Item(i);
                    FakeDatabase.Items.Insert(index, updateItem);
                }
            }

            return i;
        }
    }
}
