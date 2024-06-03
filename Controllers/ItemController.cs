using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Middlewares;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<ItemController> _logger;

        public ItemController(ApiDbContext context, ILogger<ItemController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [AuthMiddleware]
        public ActionResult<IEnumerable<Item>> GetItemsFromATodoList([FromQuery] int todolist_id)
        {
            TodoList? todoList = _context.TodoList.Include(tl => tl.Items).FirstOrDefault(tl => tl.Id == todolist_id);

            if (todoList == null)
            {
                _logger.LogDebug($"TodoList {todoList?.Id} not found");
                return Forbid();
            }

            User? user = HttpContext.Items["User"] as User;

            if (todoList.UserId != user?.Id)
            {
                _logger.LogDebug($"Unauthorized access attempt to TodoList {todoList?.Id} by User {user?.Id}");
                return Forbid();
            }

            return Ok(todoList.Items);
        }

        [HttpGet("{id}")]
        [AuthMiddleware]
        public ActionResult<Item> GetItem(int id)
        {
            Item? item = _context.Item.Include(i => i.TodoListId).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                _logger.LogDebug($"Item {item?.Id} not found");
                return Forbid();
            }

            User? user = HttpContext.Items["User"] as User;

            if (item.TodoList?.UserId != user?.Id)
            {
                _logger.LogDebug($"Unauthorized access attempt to Item {item?.Id} by User {user?.Id}");
                return Forbid();
            }

            return Ok(item);
        }

        [HttpPost]
        [AuthMiddleware]
        public ActionResult<Item> PostItem([FromBody] Item item)
        {
            TodoList? todoList = _context.TodoList.FirstOrDefault(tl => tl.Id == item.TodoListId);

            if (todoList == null)
            {
                _logger.LogDebug($"Todo list {todoList?.Id} not found");
                return Forbid();
            }

            User? user = HttpContext.Items["User"] as User;

            if (todoList.UserId != user?.Id)
            {
                _logger.LogDebug($"Unauthorized attempt to add item to TodoList {todoList?.Id} by User {user?.Id}");
                return Forbid();
            }

            _context.Item.Add(item);
            _context.SaveChanges();

            return Ok(item);
        }

        [HttpPut("{id}")]
        [AuthMiddleware]
        public ActionResult<Item> PutItem(int id, [FromBody] Item modifiedItem)
        {
            Item? item = _context.Item.Include(i => i.TodoList).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                _logger.LogDebug($"Item {item?.Id} not found");
                return Forbid();               
            }

            User? user = HttpContext.Items["User"] as User;

            if (item.TodoList?.UserId != user?.Id)
            {
                _logger.LogDebug($"Unauthorized attempt to update item {item.Id} by User {user?.Id}");
                return Forbid();                
            }

            item.Content = modifiedItem.Content;
            item.IsComplete = modifiedItem.IsComplete;
            _context.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        [AuthMiddleware]
        public IActionResult DeleteItem(int id)
        {
            var item = _context.Item.Include(i => i.TodoList).FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                _logger.LogDebug($"Item {item?.Id} not found");
                return Forbid();               
            }

            User? user = HttpContext.Items["User"] as User;

            if (item.TodoList?.UserId != user?.Id)
            {
                _logger.LogDebug($"Unauthorized attempt to update item {item.Id} by User {user?.Id}");
                return Forbid();                
            }

            _context.Item.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

