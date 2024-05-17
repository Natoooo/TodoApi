using TodoApi.Managers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ItemManager _itemManager;

        public ItemController(ItemManager itemManager, ApiDbContext context)
        {
            _context = context;
            _itemManager = itemManager;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            Console.WriteLine("Getting all items from Controller");
            return Ok(_itemManager.GetAllItems());
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            Console.WriteLine("Getting a item");
            var item = _itemManager.GetItemById(id);

            if (item == null)
            {
                Console.WriteLine("GetItemById NOT FOUND");
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Item> PostItem(Item item)
        {
            Console.WriteLine("Create a new item");
            var newItem = _itemManager.CreateItem(item);
            return Ok(newItem);
        }

        [HttpPut("{id}")]
        public ActionResult<Item> PutItem(int id, Item item)
        {
            Console.WriteLine("Update item");
            var updatedItem = _itemManager.UpdateItem(id, item);
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public void DeleteItem(int id)
        {
            Console.WriteLine("Delete item");
            _itemManager.DeleteItem(id);
        }
    }
}

