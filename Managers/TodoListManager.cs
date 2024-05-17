using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


namespace TodoApi.Managers
{
    public class TodoListManager
    {
        private readonly ApiDbContext _context;

        public TodoListManager (ApiDbContext context)
        {
            _context = context;
        }


        public IEnumerable<TodoList> GetAllTodoLists()
        {
            return _context.TodoList.Include(x => x.Items).ToList();
        }

        public TodoList? GetTodoListById(int id)
        {
            var todoList = _context.TodoList.Find(id);

            if (todoList == null)
            return null;

            return todoList;
        }

        public TodoList? CreateTodoList(TodoList todoList)
        {   
            _context.TodoList.Add(todoList);
            _context.SaveChanges();
            
            return todoList;
        }

        public TodoList? UpdateTodoList(int id, TodoList modifiedTodoList)
        {
            var todoList = _context.TodoList.Find(id);
            
            if (todoList == null)
                return null;
            
            todoList.Name = modifiedTodoList.Name;
            
            _context.SaveChanges();
            
           return todoList;
        }

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