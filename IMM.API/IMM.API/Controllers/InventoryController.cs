using Microsoft.AspNetCore.Mvc;
using IMM.Models;
using IMM.API.EC;
using IMM.DTO;

namespace IMM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpPost()]
        public async Task<ItemDTO> AddOrUpdate([FromBody] ItemDTO i)
        {
            return await new InventoryEC().AddOrUpdate(i);
        }

        [HttpDelete("/{id}")]
        public async Task<ItemDTO?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }
    }
}
