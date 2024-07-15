using Microsoft.AspNetCore.Mvc;
using IMM.Models;

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
            return new List<Item>()
            {
                new Item
                {
                    Id = 1,
                    Name = "Lightbulbs",
                    Description = "These are really good lightbulbs.",
                    Price = 30,
                    Stock = 50
                },
                new Item
                {
                    Id = 2,
                    Name = "Toothbrushes",
                    Description = "These are really good toothbrushes.",
                    Price = 10,
                    Stock = 55
                }
            };
        }
    }
}
