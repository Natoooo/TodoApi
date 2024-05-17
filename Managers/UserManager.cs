using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


namespace TodoApi.Managers
{
    public class UserManager
    {
        private readonly ApiDbContext _context;

        public UserManager (ApiDbContext context)
        {
            _context = context;
        }

        public void InitContext()
        {
            if (_context.User.Count() == 0)
            {
                _context.User.Add(new User { Email = "lana.prout@gmail.com", Password = "proutprout", TodoLists = {
                    new TodoList { Name = "Movies", Items = { 
                        new Item { Content = "Titanic", IsComplete = true },
                        new Item { Content = "Rambo", IsComplete = false },
                        new Item { Content = "Forest Gump", IsComplete = true }}},
                    new TodoList { Name = "Travels", Items = { 
                        new Item { Content = "Malaisia", IsComplete = true },
                        new Item { Content = "Costa Rica", IsComplete = false },
                        new Item { Content = "Bali", IsComplete = true }}}}}); 

                _context.User.Add(new User { Email = "alfredo.pipi@gmail.com", Password = "pipipipi", TodoLists = {
                    new TodoList { Name = "Desserts", Items = { 
                        new Item { Content = "Tiramisu", IsComplete = true },
                        new Item { Content = "Cheesecake", IsComplete = false },
                        new Item { Content = "Brownie", IsComplete = true }}}}});
                
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.User.Include(x => x.TodoLists).ThenInclude(c => c.Items).ToList();
        }

        public User? GetUserById(int id)
        {
            var user = _context.User.Find(id);

            if (user == null)
            return null;

            return user;
        }

        public User? CreateUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            
            return user;
        }

        public User? UpdateUser(int id, User modifiedUser)
        {
            var user = _context.User.Find(id);
            
            if (user == null)
                return null;
            
            user.Email = modifiedUser.Email;
            
            _context.SaveChanges();
            
           return user;
        }

        public void DeleteUser(int id)
        {
            var user = _context.User.Find(id);

            if (user == null)
                throw new Exception("User doesn't exist");

            _context.User.Remove(user);
            _context.SaveChanges();
        }
    }
}