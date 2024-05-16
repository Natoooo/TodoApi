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

        public void InitContext()
        {
            if (_context.TodoList.Count() == 0)
            {
                var todoLists = new List<TodoList> {
                    new TodoList{Name = "Movies"},
                    new TodoList{Name = "Travels"}
                };

                var items = new List<List<Item>> {
                    new List<Item>{
                        new Item { Content = "Titanic", IsComplete = true },
                        new Item { Content = "Rambo", IsComplete = false },
                        new Item { Content = "Forest Gump", IsComplete = true } 
                    },
                    new List<Item>{
                        new Item { Content = "Malaisia", IsComplete = true },
                        new Item { Content = "Costa Rica", IsComplete = false },
                        new Item { Content = "Bali", IsComplete = true }
                    }
                };

                for (int i = 0; i < todoLists.Count; i++) {
                    _context.TodoList.Add(todoLists[i]);
                }

                _context.SaveChanges();


                for (int i = 0; i < todoLists.Count; i++) {
                    for (int j = 0; j < items[i].Count; j++) {
                        var item = items[i][j];

                        item.TodoListId = todoLists[i].Id;
                        _context.Item.Add(item);
                    }
                }

                _context.SaveChanges();





                /*_context.TodoList.Add(new TodoList { Name = "Movies", Items = { 
                    new Item { Content = "Titanic", IsComplete = true },
                    new Item { Content = "Rambo", IsComplete = false },
                    new Item { Content = "Forest Gump", IsComplete = true }}}); 

                _context.TodoList.Add(new TodoList { Name = "Travels", Items = { 
                    new Item { Content = "Malaisia", IsComplete = true },
                    new Item { Content = "Costa Rica", IsComplete = false },
                    new Item { Content = "Bali", IsComplete = true }}});*/


            }
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
                throw(new Exception("TodoList doesn't exist"));

            _context.TodoList.Remove(todoList);
            _context.SaveChanges();
        }
    }
}