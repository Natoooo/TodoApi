using TodoApi.Managers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/todoLists")]
    public class TodoListController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly TodoListManager _todoListManager;

        public TodoListController(TodoListManager todoListManager, ApiDbContext context)
        {
            _context = context;
            _todoListManager = todoListManager;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<TodoList>> GetTodoLists()
        {

            Console.WriteLine("Getting all todoLists");
            return Ok(_todoListManager.GetAllTodoLists());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoList> GetTodoList(int id)
        {
            Console.WriteLine("Getting a todoList");
            var todoList = _todoListManager.GetTodoListById(id);

            if (todoList == null)
            {
                Console.WriteLine("GetTodListById NOT FOUND");
                return NotFound();
            }

            return Ok(todoList);
        }

        [HttpPost]
        public ActionResult<TodoList> PostTodoList(TodoList todoList)
        {
            Console.WriteLine("Create a new todoList");
            var newTodoList = _todoListManager.CreateTodoList(todoList);
            return Ok(newTodoList);
        }

        [HttpPut("{id}")]
        public ActionResult<TodoList> PutTodoList(int id, TodoList todoList)
        {
            Console.WriteLine("Update todoList");
            var updatedTodoList = _todoListManager.UpdateTodoList(id, todoList);
            return Ok(updatedTodoList);
        }

        [HttpDelete("{id}")]
        public void DeleteTodoList(int id)
        {
            Console.WriteLine("Delete todoList");
            _todoListManager.DeleteTodoList(id);
        }
    }
}
