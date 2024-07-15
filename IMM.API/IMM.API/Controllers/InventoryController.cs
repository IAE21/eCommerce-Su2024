using Microsoft.AspNetCore.Mvc;
using IMM.Models;
using IMM.API.EC;

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
        public async Task<IEnumerable<Item>> Get()
        {
            return await new InventoryEC().Get();
        }
    }
}
