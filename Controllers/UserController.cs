using TodoApi.Managers;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;


namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly UserManager _userManager;

        public UserController(UserManager userManager, ApiDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _userManager.InitContext();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {

            Console.WriteLine("Getting all users");
            return Ok(_userManager.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            Console.WriteLine("Getting a user");
            var user = _userManager.GetUserById(id);

            if (user == null)
            {
                Console.WriteLine("GetUserById NOT FOUND");
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            Console.WriteLine("Create a new todoList");
            var newUser = _userManager.CreateUser(user);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public ActionResult<User> PutUser(int id, User user)
        {
            Console.WriteLine("Update user");
            var updatedUser = _userManager.UpdateUser(id, user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            Console.WriteLine("Delete user");
            _userManager.DeleteUser(id);
        }
    }
}