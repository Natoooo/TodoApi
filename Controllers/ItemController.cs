using Microsoft.AspNetCore.Mvc;
using TodoApi.Middlewares;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ItemController(ApiDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [AuthMiddleware]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            var user = HttpContext.Items["User"];
            var items =_context.Item.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [AuthMiddleware]
        public ActionResult<Item> GetItem(int id)
        {
            var item = _context.Item.Find(id);

            if (item == null)
            {
                Console.WriteLine("GetItemById NOT FOUND");
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [AuthMiddleware]
        public ActionResult<Item> PostItem(Item item)
        {
            var newItem = _context.Item.Add(item);
            _context.SaveChanges();          
            return Ok(newItem);
        }

        [HttpPut("{id}")]
        [AuthMiddleware]
        public ActionResult<Item>? PutItem(int id, Item modifiedItem)
        {
            var item = _context.Item.Find(id);
            
            if (item == null)
                return null;
            
            item.Content = modifiedItem.Content;
            item.IsComplete = modifiedItem.IsComplete;          
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        [AuthMiddleware]
        public void DeleteItem(int id)
        {
            var item = _context.Item.Find(id);

            if (item == null)
                throw new Exception("Item doesn't exist");

            _context.Item.Remove(item);
            _context.SaveChanges();
        }
    }
}

