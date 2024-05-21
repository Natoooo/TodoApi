using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using TodoApi.Middlewares;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
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


        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.User.Include(x => x.TodoLists).ThenInclude(c => c.Items).ToList();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> SignUp(User user)
        {
            var newUser = _context.User.Add(user);
            _context.SaveChanges();
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        [AuthMiddleware]
        public ActionResult<User>? PutUser(int id, User modifiedUser)
        {
            var user = _context.User.Find(id);
            
            if (user == null)
                return null;
            
            user.Email = modifiedUser.Email;           
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            var user = _context.User.Find(id);

            if (user == null)
                throw new Exception("TodoList doesn't exist");

            _context.User.Remove(user);
            _context.SaveChanges();
        }
    }
}