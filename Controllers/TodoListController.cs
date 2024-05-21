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

        public TodoListController(ApiDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        [AuthMiddleware]
        public ActionResult<IEnumerable<TodoList>>? GetTodoLists()
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
                return null;

            //var todoLists = _context.TodoList.Include(x => x.Items).ToList();
            List<TodoList> todoLists = _context.TodoList.Include(x => x.Items).Where(x => x.UserId == user.Id).ToList();
            return Ok(todoLists);
        }

        [HttpGet("{id}")]
        [AuthMiddleware]
        public ActionResult<TodoList>? GetTodoList(int id)
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null)
                return null;

            TodoList? todoList= _context.TodoList.Find(id);
            
            if (todoList == null)
            {
                Console.WriteLine("GetTodoListById NOT FOUND");
                return NotFound();
            }

            if (todoList.UserId != user.Id )
                return Unauthorized();
            
            _context.TodoList.Include(x => x.Items).ToList();
            return Ok(todoList);
        }

        [HttpPost]
        [AuthMiddleware]
        public ActionResult<TodoList>? PostTodoList(TodoList todoList)
        {
            User? user = HttpContext.Items["User"] as User;
            
            if (user == null)    
                return null;
            
            _context.TodoList.Add(todoList);
            
            if (todoList.UserId != user.Id )
                return Unauthorized();

            
            todoList.UserId = user.Id;
            
            _context.SaveChanges();
            return Ok(todoList);
        }

        [HttpPut("{id}")]
        [AuthMiddleware]
        public ActionResult<TodoList>? PutTodoList(int id, TodoList modifiedTodoList)
        {
            var todoList = _context.TodoList.Find(id);
            
            if (todoList == null)
                return null;
            
            todoList.Name = modifiedTodoList.Name;           
            _context.SaveChanges();
            return Ok(todoList);
        }

        [HttpDelete("{id}")]
        [AuthMiddleware]
        public void DeleteTodoList(int id)
        {
            var todoList = _context.TodoList.Find(id);

            if (todoList == null)
                throw new Exception("TodoList doesn't exist");

            _context.TodoList.Remove(todoList);
            _context.SaveChanges();
        }
    }
}
