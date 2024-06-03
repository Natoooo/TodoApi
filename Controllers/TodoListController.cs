using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using TodoApi.Middlewares;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/todoLists")]
    public class TodoListController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<TodoListController> _logger;

        public TodoListController(ApiDbContext context, ILogger<TodoListController> logger)
        {
            _context = context;
            _logger = logger;
        }
        

        [HttpGet]
        [AuthMiddleware]
        public ActionResult<IEnumerable<TodoList>>? GetTodoLists()
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
            {
                _logger.LogDebug("User is null in GetTodoLists");
                return Forbid();
            }

            List<TodoList> todoLists = _context.TodoList.Where(x => x.UserId == user.Id).ToList();           
            return Ok(todoLists);
        }

        [HttpGet("{id}")]
        [AuthMiddleware]
        public ActionResult<TodoList>? GetTodoList(int id)
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
            {
                _logger.LogDebug("User is null in GetTodoList");
                return Forbid();
            }
                
            TodoList? todoList = _context.TodoList.FirstOrDefault(tl => tl.Id == id);
            
            if (todoList?.UserId != user.Id )
            {
                _logger.LogDebug($"Unauthorized access attempt to TodoList {todoList?.Id} by User {user.Id}");
                return Unauthorized();

            }
                
            _context.TodoList.Include(x => x.Items).ToList();
            return Ok(todoList);
        }

        [HttpPost]
        [AuthMiddleware]
        public ActionResult<TodoList>? PostTodoList([FromBody] TodoList todoList)
        {
            User? user = HttpContext.Items["User"] as User;
            
            if (user == null)
            {
                _logger.LogDebug("User is null in PostTodoList");
                return Forbid();
            }   
            
            todoList.UserId = user.Id;
            _context.TodoList.Add(todoList);            
            _context.SaveChanges();
            return Ok(todoList);
        }

        [HttpPut("{id}")]
        [AuthMiddleware]
        public ActionResult<TodoList>? PutTodoList(int id, [FromBody] TodoList modifiedTodoList)
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
            {
                _logger.LogDebug("User is null in PutTodoList");
                return Forbid();
            }

            TodoList? todoList = _context.TodoList.FirstOrDefault(tl => tl.Id == id);

            if (todoList?.UserId != user.Id)
            {
                _logger.LogDebug($"Unauthorized update attempt to TodoList {todoList?.Id} by User {user.Id}");
                return Forbid();
            }

            todoList.Name = modifiedTodoList.Name;
            _context.SaveChanges();
            return Ok(todoList);
        }

        [HttpDelete("{id}")]
        [AuthMiddleware]
        public IActionResult DeleteTodoList(int id)
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
            {
                _logger.LogDebug("User is null in DeleteTodoList");
                return Forbid();
            }

            TodoList? todoList = _context.TodoList.FirstOrDefault(tl => tl.Id == id);

            if (todoList?.UserId != user.Id)
            {
                _logger.LogDebug($"Unauthorized update attempt to TodoList {todoList?.Id} by User {user.Id}");
                return Forbid();
            }

            _context.TodoList.Remove(todoList);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
